using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using PDV.Api.Common;
using PDV.Api.Domain;
using PDV.Api.Fiscal.DTOs;
using PDV.Api.Fiscal.Providers;
using PDV.Api.Services;
using System.Globalization;

namespace PDV.Api.Fiscal.Providers.NuvemFiscal;

public class NuvemFiscalProvider(
    FiscalProviderContext context,
    IHttpClientFactory httpClientFactory,
    NuvemFiscalAuthService authService,
    NfeCertificateService nfeCertificateService) : ExternalFiscalProviderBase(context, httpClientFactory)
{
    private const string DefaultSandboxBaseUrl = "https://api.sandbox.nuvemfiscal.com.br";
    private const string DefaultProductionBaseUrl = "https://api.nuvemfiscal.com.br";

    private sealed record ResolvedNFeDocument(
        string Referencia,
        string DocumentoId,
        string RawJson);

    public override FiscalProvider Kind => FiscalProvider.NuvemFiscal;
    public override string DisplayName => "Nuvem Fiscal";

    private async Task<ResolvedNFeDocument> WaitForNFeCompletionAsync(
      HttpClient client,
      string referencia,
      string documentoId,
      int maxAttempts = 8,
      CancellationToken cancellationToken = default)
    {
        string? lastBody = null;

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            using var response = await client.GetAsync(
                $"/nfe/{Uri.EscapeDataString(documentoId)}",
                cancellationToken);

            lastBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new AppException(
                    $"Nao foi possivel consultar a NF-e em processamento: {Summarize(lastBody)}");
            }

            using var json = JsonDocument.Parse(lastBody);
            var root = json.RootElement;

            var status = MapStatus(
                ReadString(root, "status"),
                ReadNestedNullableInt(root, "autorizacao", "codigo_status")
                    ?? ReadNullableInt(root, "codigo_status"));

            var resolvedReference =
                ReadString(root, "referencia") ?? referencia;

            var resolvedId =
                ReadString(root, "id") ?? documentoId;

            if (status is FiscalDocumentoStatus.Autorizada
                or FiscalDocumentoStatus.Rejeitada
                or FiscalDocumentoStatus.Cancelada)
            {
                return new ResolvedNFeDocument(
                    resolvedReference,
                    resolvedId,
                    lastBody);
            }

            if (attempt < maxAttempts)
            {
                var delay = attempt switch
                {
                    1 => TimeSpan.FromSeconds(1),
                    2 => TimeSpan.FromSeconds(2),
                    3 => TimeSpan.FromSeconds(3),
                    4 => TimeSpan.FromSeconds(5),
                    5 => TimeSpan.FromSeconds(7),
                    _ => TimeSpan.FromSeconds(8)
                };

                await Task.Delay(delay, cancellationToken);
            }
        }

        // IMPORTANTE:
        // Timeout de polling NÃO significa rejeição.
        // A Nuvem Fiscal pode continuar processando.
        return new ResolvedNFeDocument(
            referencia,
            documentoId,
            lastBody ?? JsonSerializer.Serialize(new
            {
                id = documentoId,
                referencia,
                status = "pendente"
            }));
    }

    public override async Task<EmitirNFeResult> EmitirNFeAsync(
    EmitirNFeRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Documento);

        var document = request.Documento;

        ValidateDocumentForEmission(document);

        var baseUrl = ResolveBaseUrl();

        using var client = await CreateAuthorizedClientAsync(baseUrl);

        await EnsureProviderReadyAsync(client);

        var providerPayload = BuildProviderPayload(document);

        using var response = await client.PostAsync(
            "/nfe",
            JsonContent(providerPayload));

        var initialBody =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new AppException(
                $"A Nuvem Fiscal rejeitou a solicitacao de emissao: {Summarize(initialBody)}");
        }

        using var initialJson = JsonDocument.Parse(initialBody);

        var initialRoot = initialJson.RootElement;

        var initialDocumentoId =
            ReadString(initialRoot, "id");

        if (string.IsNullOrWhiteSpace(initialDocumentoId))
        {
            throw new AppException(
                "A Nuvem Fiscal recebeu a NF-e, mas nao retornou o identificador do documento.");
        }

        var initialStatus = MapStatus(
            ReadString(initialRoot, "status"),
            ReadNestedNullableInt(
                initialRoot,
                "autorizacao",
                "codigo_status")
            ?? ReadNullableInt(
                initialRoot,
                "codigo_status"));

        ResolvedNFeDocument resolved;

        if (initialStatus == FiscalDocumentoStatus.Pendente)
        {
            resolved = await WaitForNFeCompletionAsync(
                client,
                document.Referencia,
                initialDocumentoId,
                cancellationToken: CancellationToken.None);
        }
        else
        {
            resolved = new ResolvedNFeDocument(
                ReadString(initialRoot, "referencia")
                    ?? document.Referencia,
                initialDocumentoId,
                initialBody);
        }

        // ESTE era um dos bugs do seu código:
        // depois do polling você estava parseando responseBody,
        // que era o retorno inicial.
        using var json =
            JsonDocument.Parse(resolved.RawJson);

        var root =
            json.RootElement;

        var resolvedDocumentoId =
            ReadString(root, "id")
            ?? resolved.DocumentoId;

        var status =
            MapStatus(
                ReadString(root, "status"),
                ReadNestedNullableInt(
                    root,
                    "autorizacao",
                    "codigo_status")
                ?? ReadNullableInt(
                    root,
                    "codigo_status"));

        var chave =
            ReadString(root, "chave")
            ?? ReadNestedString(
                root,
                "autorizacao",
                "chave_acesso");

        var protocolo =
            ReadNestedString(
                root,
                "autorizacao",
                "numero_protocolo");

        var codigoStatus =
            ReadNestedNullableInt(
                root,
                "autorizacao",
                "codigo_status")
            ?? ReadNullableInt(
                root,
                "codigo_status");

        var mensagem =
            ReadNestedString(
                root,
                "autorizacao",
                "motivo_status")
            ?? ReadNestedString(
                root,
                "autorizacao",
                "mensagem")
            ?? ReadString(root, "motivo_status")
            ?? ReadString(root, "mensagem")
            ?? ReadString(root, "status")
            ?? "Solicitacao recebida pela Nuvem Fiscal.";

        var finalizado =
            status is FiscalDocumentoStatus.Autorizada
                or FiscalDocumentoStatus.Rejeitada
                or FiscalDocumentoStatus.Cancelada;

        string? xml = null;
        string? danfeUrl = null;
        string? danfePdfBase64 = null;

        // Não tenta baixar XML/PDF enquanto ainda está pendente.
        if (finalizado &&
            !string.IsNullOrWhiteSpace(resolvedDocumentoId))
        {
            danfeUrl =
                $"{baseUrl}/nfe/{resolvedDocumentoId}/pdf";

            var xmlUrl =
                $"{baseUrl}/nfe/{resolvedDocumentoId}/xml";

            xml =
                await TryDownloadTextAsync(
                    client,
                    xmlUrl);

            danfePdfBase64 =
                await TryDownloadBinaryAsBase64Async(
                    client,
                    danfeUrl);
        }

        return new EmitirNFeResult(
            status,
            mensagem,
            resolved.Referencia,
            resolvedDocumentoId,
            codigoStatus,
            chave,
            protocolo,
            xml,
            danfeUrl,
            danfePdfBase64,
            SerializeJson(document),
            SerializeJson(providerPayload),
            resolved.RawJson,
            DateTime.UtcNow,
            ParseDate(
                ReadNestedString(
                    root,
                    "autorizacao",
                    "data_recebimento")),
            finalizado);
    }

    public override async Task<ConsultarNFeResult> ConsultarNFeAsync(string referenciaOuId)
    {
        var baseUrl = ResolveBaseUrl();
        using var client = await CreateAuthorizedClientAsync(baseUrl);
        var resolved = await ResolveNFeAsync(client, referenciaOuId);
        return await MapConsultarResultAsync(client, baseUrl, resolved);
    }

    public override async Task<ConsultarNFeResult> SincronizarNFeAsync(string referenciaOuId)
    {
        var baseUrl = ResolveBaseUrl();
        using var client = await CreateAuthorizedClientAsync(baseUrl);
        var resolved = await ResolveNFeAsync(client, referenciaOuId);

        using var response = await client.PostAsync($"/nfe/{resolved.DocumentoId}/sincronizar", JsonContent(new { }));
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new AppException($"A Nuvem Fiscal nao conseguiu sincronizar a NF-e com a SEFAZ: {Summarize(responseBody)}");
        }

        var refreshed = await ResolveNFeAsync(client, resolved.DocumentoId);
        return await MapConsultarResultAsync(client, baseUrl, refreshed);
    }

    public override async Task<CancelarNFeResult> CancelarNFeAsync(CancelarNFeRequest request)
    {
        var baseUrl = ResolveBaseUrl();
        using var client = await CreateAuthorizedClientAsync(baseUrl);
        var resolved = await ResolveNFeAsync(client, request.ReferenciaOuId);

        using var response = await client.PostAsync(
            $"/nfe/{resolved.DocumentoId}/cancelamento",
            JsonContent(new { justificativa = request.Justificativa }));
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new AppException($"A Nuvem Fiscal rejeitou o cancelamento da NF-e: {Summarize(responseBody)}");
        }

        using var json = JsonDocument.Parse(responseBody);
        var root = json.RootElement;
        var codigoStatus = ReadNullableInt(root, "codigo_status");
        var mensagem = ReadString(root, "motivo_status")
            ?? ReadString(root, "mensagem")
            ?? "Solicitacao de cancelamento enviada para a Nuvem Fiscal.";
        var status = MapEventStatus(ReadString(root, "status"), codigoStatus, FiscalDocumentoStatus.Cancelada);

        return new CancelarNFeResult(
            status,
            mensagem,
            resolved.Referencia,
            resolved.DocumentoId,
            ReadString(root, "id"),
            codigoStatus,
            ReadString(root, "numero_protocolo"),
            ReadString(root, "chave_acesso"),
            responseBody,
            ParseDate(ReadString(root, "data_recebimento")),
            status is FiscalDocumentoStatus.Cancelada or FiscalDocumentoStatus.Rejeitada);
    }

    public override async Task<FiscalEventoNFeResult> SolicitarCartaCorrecaoNFeAsync(CartaCorrecaoNFeRequest request)
    {
        var normalizedCorrection = request.Correcao?.Trim();
        if (string.IsNullOrWhiteSpace(normalizedCorrection))
        {
            throw new AppException("Informe o texto da carta de correcao.");
        }

        var baseUrl = ResolveBaseUrl();
        using var client = await CreateAuthorizedClientAsync(baseUrl);
        var resolved = await ResolveNFeAsync(client, request.ReferenciaOuId);

        using var response = await client.PostAsync(
            $"/nfe/{resolved.DocumentoId}/carta-correcao",
            JsonContent(new { correcao = normalizedCorrection }));
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new AppException($"A Nuvem Fiscal rejeitou a carta de correcao da NF-e: {Summarize(responseBody)}");
        }

        return MapEventResult(
            resolved.Referencia,
            resolved.DocumentoId,
            responseBody,
            "Carta de correcao enviada para a Nuvem Fiscal.",
            FiscalDocumentoStatus.Autorizada);
    }

    public override async Task<EnviarEmailNFeResult> EnviarEmailNFeAsync(EnviarEmailNFeRequest request)
    {
        var baseUrl = ResolveBaseUrl();
        using var client = await CreateAuthorizedClientAsync(baseUrl);
        var resolved = await ResolveNFeAsync(client, request.ReferenciaOuId);

        object payload = request.Destinatarios.Count == 0
            ? new { }
            : new
            {
                destinatarios = request.Destinatarios
                    .Select(item => item.Trim())
                    .Where(item => !string.IsNullOrWhiteSpace(item))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Select(item => new { email = item })
                    .ToArray()
            };

        using var response = await client.PostAsync($"/nfe/{resolved.DocumentoId}/email", JsonContent(payload));
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new AppException($"A Nuvem Fiscal nao conseguiu enviar o XML/PDF da NF-e por e-mail: {Summarize(responseBody)}");
        }

        using var json = JsonDocument.Parse(responseBody);
        var root = json.RootElement;
        return new EnviarEmailNFeResult(
            ReadString(root, "status_message") ?? "Solicitacao de envio por e-mail recebida pela Nuvem Fiscal.",
            resolved.Referencia,
            resolved.DocumentoId,
            ReadString(root, "status") ?? "pending",
            responseBody);
    }

    public override async Task<BaixarNFeDocumentoResult> BaixarDocumentoNFeAsync(BaixarNFeDocumentoRequest request)
    {
        var baseUrl = ResolveBaseUrl();
        using var client = await CreateAuthorizedClientAsync(baseUrl);
        var resolved = await ResolveNFeAsync(client, request.ReferenciaOuId);
        var relativePath = BuildDocumentPath(resolved.DocumentoId, request);
        var absoluteUrl = BuildAbsoluteUrl(baseUrl, relativePath);

        using var response = await client.GetAsync(relativePath);
        var mediaType = response.Content.Headers.ContentType?.MediaType ?? GuessMediaType(request.Tipo);
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            return new BaixarNFeDocumentoResult(
                resolved.Referencia,
                resolved.DocumentoId,
                request.Tipo,
                mediaType,
                BuildFileName(resolved, request.Tipo, mediaType),
                null,
                null,
                absoluteUrl,
                false,
                $"Documento ainda nao disponivel na Nuvem Fiscal: {Summarize(errorBody)}");
        }

        if (IsTextLikeContent(mediaType, request.Tipo))
        {
            var content = await response.Content.ReadAsStringAsync();
            return new BaixarNFeDocumentoResult(
                resolved.Referencia,
                resolved.DocumentoId,
                request.Tipo,
                mediaType,
                BuildFileName(resolved, request.Tipo, mediaType),
                null,
                content,
                absoluteUrl,
                true,
                "Documento obtido com sucesso.");
        }

        var bytes = await response.Content.ReadAsByteArrayAsync();
        return new BaixarNFeDocumentoResult(
            resolved.Referencia,
            resolved.DocumentoId,
            request.Tipo,
            mediaType,
            BuildFileName(resolved, request.Tipo, mediaType),
            bytes.Length == 0 ? null : Convert.ToBase64String(bytes),
            null,
            absoluteUrl,
            bytes.Length > 0,
            bytes.Length > 0
                ? "Documento obtido com sucesso."
                : "A Nuvem Fiscal respondeu sem conteudo para o documento solicitado.");
    }

    public override async Task<InutilizarNFeResult> InutilizarNumeracaoNFeAsync(InutilizarNFeRequest request)
    {
        using var client = await CreateAuthorizedClientAsync(ResolveBaseUrl());
        await EnsureProviderReadyAsync(client);

        using var response = await client.PostAsync(
            "/nfe/inutilizacoes",
            JsonContent(new
            {
                ambiente = AmbienteLiteral(Context.Ambiente),
                cnpj = Context.Cnpj,
                ano = request.Ano,
                serie = request.Serie,
                numero_inicial = request.NumeroInicial,
                numero_final = request.NumeroFinal,
                justificativa = request.Justificativa
            }));
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new AppException($"A Nuvem Fiscal rejeitou a inutilizacao da numeracao da NF-e: {Summarize(responseBody)}");
        }

        using var json = JsonDocument.Parse(responseBody);
        var root = json.RootElement;
        var codigoStatus = ReadNullableInt(root, "codigo_status");
        var mensagem = ReadString(root, "motivo_status")
            ?? ReadString(root, "mensagem")
            ?? "Pedido de inutilizacao recebido pela Nuvem Fiscal.";
        var status = MapEventStatus(ReadString(root, "status"), codigoStatus, FiscalDocumentoStatus.Autorizada);

        return new InutilizarNFeResult(
            status,
            mensagem,
            ReadString(root, "id"),
            ReadString(root, "tipo_evento"),
            codigoStatus,
            ReadString(root, "numero_protocolo"),
            ReadString(root, "chave_acesso"),
            responseBody,
            ParseDate(ReadString(root, "data_recebimento")),
            status is FiscalDocumentoStatus.Autorizada or FiscalDocumentoStatus.Rejeitada);
    }

    public override async Task<StatusServicoResult> ConsultarStatusServicoAsync()
    {
        var baseUrl = ResolveBaseUrl();
        using var client = await CreateAuthorizedClientAsync(baseUrl);
        await EnsureProviderReadyAsync(client);

        var query = $"/nfe/sefaz/status?cpf_cnpj={Context.Cnpj}&autorizador={Context.Uf}";
        using var response = await client.GetAsync(query);
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new AppException($"Nao foi possivel consultar o status da SEFAZ na Nuvem Fiscal: {Summarize(responseBody)}");
        }

        using var json = JsonDocument.Parse(responseBody);
        var root = json.RootElement;
        var codigoStatus = ReadNullableInt(root, "codigo_status");
        var mensagem = ReadString(root, "motivo_status") ?? "Status consultado com sucesso.";

        return new StatusServicoResult(
            codigoStatus == 107,
            codigoStatus,
            mensagem,
            $"{baseUrl}/nfe/sefaz/status",
            ParseDate(ReadString(root, "data_hora_retorno")),
            responseBody);
    }

    private async Task<HttpClient> CreateAuthorizedClientAsync(string baseUrl)
    {
        var client = CreateJsonClient(baseUrl);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await authService.GetAccessTokenAsync(Context));
        return client;
    }

    private async Task<ConsultarNFeResult> MapConsultarResultAsync(HttpClient client, string baseUrl, ResolvedNFeDocument resolved)
    {
        using var json = JsonDocument.Parse(resolved.RawJson);
        var root = json.RootElement;
        var documentoId = ReadString(root, "id") ?? resolved.DocumentoId;
        var status = MapStatus(ReadString(root, "status"), ReadNestedNullableInt(root, "autorizacao", "codigo_status"));
        var chave = ReadString(root, "chave") ?? ReadNestedString(root, "autorizacao", "chave_acesso");
        var protocolo = ReadNestedString(root, "autorizacao", "numero_protocolo");
        var codigoStatus = ReadNestedNullableInt(root, "autorizacao", "codigo_status") ?? ReadNullableInt(root, "codigo_status");
        var mensagem = ReadNestedString(root, "autorizacao", "motivo_status")
            ?? ReadString(root, "motivo_status")
            ?? ReadString(root, "status")
            ?? "Consulta realizada com sucesso.";

        var xmlUrl = string.IsNullOrWhiteSpace(documentoId) ? null : $"{baseUrl}/nfe/{documentoId}/xml";
        var danfeUrl = string.IsNullOrWhiteSpace(documentoId) ? null : $"{baseUrl}/nfe/{documentoId}/pdf";
        var xml = await TryDownloadTextAsync(client, xmlUrl);
        var danfePdfBase64 = await TryDownloadBinaryAsBase64Async(client, danfeUrl);

        return new ConsultarNFeResult(
            status,
            mensagem,
            ReadString(root, "referencia") ?? resolved.Referencia,
            documentoId,
            codigoStatus,
            chave,
            protocolo,
            xml,
            danfeUrl,
            danfePdfBase64,
            resolved.RawJson,
            ParseDate(ReadNestedString(root, "autorizacao", "data_recebimento")),
            status is FiscalDocumentoStatus.Autorizada or FiscalDocumentoStatus.Rejeitada or FiscalDocumentoStatus.Cancelada);
    }

    private async Task<ResolvedNFeDocument> ResolveNFeAsync(HttpClient client, string referenciaOuId)
    {
        var normalized = string.IsNullOrWhiteSpace(referenciaOuId)
            ? throw new AppException("Informe a referencia ou o identificador da NF-e.")
            : referenciaOuId.Trim();

        var byReference = await TryGetNotaRawByReferenceAsync(client, normalized);
        if (byReference is not null)
        {
            using var referenceJson = JsonDocument.Parse(byReference);
            var documentId = ReadString(referenceJson.RootElement, "id");
            var reference = ReadString(referenceJson.RootElement, "referencia") ?? normalized;
            if (string.IsNullOrWhiteSpace(documentId))
            {
                throw new AppException("A Nuvem Fiscal retornou a NF-e sem o identificador do documento.");
            }

            var byId = await TryGetNotaRawByIdAsync(client, documentId);
            return new ResolvedNFeDocument(reference, documentId, byId ?? byReference);
        }

        var byIdOnly = await TryGetNotaRawByIdAsync(client, normalized);
        if (byIdOnly is null)
        {
            throw new NotFoundException("A Nuvem Fiscal ainda nao localizou a NF-e pela referencia ou identificador informado.");
        }

        using var idJson = JsonDocument.Parse(byIdOnly);
        var referencia = ReadString(idJson.RootElement, "referencia") ?? normalized;
        var documentoIdFromPayload = ReadString(idJson.RootElement, "id") ?? normalized;
        return new ResolvedNFeDocument(referencia, documentoIdFromPayload, byIdOnly);
    }

    private async Task<string?> TryGetNotaRawByReferenceAsync(HttpClient client, string referencia)
    {
        var query = $"/nfe?cpf_cnpj={Context.Cnpj}&ambiente={AmbienteLiteral(Context.Ambiente)}&referencia={Uri.EscapeDataString(referencia)}";
        using var response = await client.GetAsync(query);
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new AppException($"Nao foi possivel consultar a NF-e na Nuvem Fiscal: {Summarize(responseBody)}");
        }

        using var json = JsonDocument.Parse(responseBody);
        if (!json.RootElement.TryGetProperty("data", out var dataNode) || dataNode.ValueKind != JsonValueKind.Array)
        {
            return null;
        }

        foreach (var item in dataNode.EnumerateArray())
        {
            return item.GetRawText();
        }

        return null;
    }

    private async Task<string?> TryGetNotaRawByIdAsync(HttpClient client, string documentoId)
    {
        using var response = await client.GetAsync($"/nfe/{Uri.EscapeDataString(documentoId)}");
        var responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return responseBody;
        }

        if (IsNotFoundResponse(response.StatusCode, responseBody))
        {
            return null;
        }

        throw new AppException($"Nao foi possivel consultar a NF-e pelo identificador na Nuvem Fiscal: {Summarize(responseBody)}");
    }

    private static FiscalEventoNFeResult MapEventResult(
        string referencia,
        string documentoId,
        string responseBody,
        string defaultMessage,
        FiscalDocumentoStatus successStatus)
    {
        using var json = JsonDocument.Parse(responseBody);
        var root = json.RootElement;
        var codigoStatus = ReadNullableInt(root, "codigo_status");
        var mensagem = ReadString(root, "motivo_status")
            ?? ReadString(root, "mensagem")
            ?? defaultMessage;
        var status = MapEventStatus(ReadString(root, "status"), codigoStatus, successStatus);

        return new FiscalEventoNFeResult(
            status,
            mensagem,
            referencia,
            documentoId,
            ReadString(root, "id"),
            ReadString(root, "tipo_evento"),
            codigoStatus,
            ReadString(root, "numero_protocolo"),
            ReadString(root, "chave_acesso"),
            responseBody,
            ParseDate(ReadString(root, "data_recebimento")),
            status is not FiscalDocumentoStatus.Pendente);
    }

    private static FiscalDocumentoStatus MapEventStatus(
    string? status,
    int? code,
    FiscalDocumentoStatus successStatus)
    {
        var normalized =
            status?
                .Trim()
                .ToLowerInvariant();

        if (normalized is
            "autorizado" or
            "autorizada" or
            "concluido" or
            "concluida" or
            "registrado" or
            "registrada")
        {
            return successStatus;
        }

        if (normalized is
            "erro" or
            "error" or
            "rejeitado" or
            "rejeitada" or
            "denegado" or
            "denegada")
        {
            return FiscalDocumentoStatus.Rejeitada;
        }

        return code switch
        {
            100 or
            101 or
            102 or
            128 or
            135 or
            136 or
            150 or
            151 or
            155 =>
                successStatus,

            105 or
            106 or
            107 or
            108 or
            109 =>
                FiscalDocumentoStatus.Pendente,

            110 =>
                FiscalDocumentoStatus.Rejeitada,

            >= 200 =>
                FiscalDocumentoStatus.Rejeitada,

            _ =>
                FiscalDocumentoStatus.Pendente
        };
    }

    private static string BuildDocumentPath(string documentoId, BaixarNFeDocumentoRequest request)
        => request.Tipo switch
        {
            NFeDownloadDocumentoTipo.DanfePdf => BuildDanfePath(documentoId, request),
            NFeDownloadDocumentoTipo.XmlProcessado => $"/nfe/{documentoId}/xml",
            NFeDownloadDocumentoTipo.XmlNota => $"/nfe/{documentoId}/xml/nota",
            NFeDownloadDocumentoTipo.XmlProtocolo => $"/nfe/{documentoId}/xml/protocolo",
            NFeDownloadDocumentoTipo.CancelamentoPdf => $"/nfe/{documentoId}/cancelamento/pdf",
            NFeDownloadDocumentoTipo.CancelamentoXml => $"/nfe/{documentoId}/cancelamento/xml",
            NFeDownloadDocumentoTipo.CartaCorrecaoPdf => $"/nfe/{documentoId}/carta-correcao/pdf",
            NFeDownloadDocumentoTipo.CartaCorrecaoXml => $"/nfe/{documentoId}/carta-correcao/xml",
            _ => throw new AppException("Tipo de download da NF-e nao reconhecido.")
        };

    private static string BuildDanfePath(string documentoId, BaixarNFeDocumentoRequest request)
    {
        var query = new List<string>();
        if (request.IncluirLogotipo)
        {
            query.Add("logotipo=true");
        }

        if (request.ExibirNomeFantasia)
        {
            query.Add("nome_fantasia=true");
        }

        if (!string.IsNullOrWhiteSpace(request.Formato))
        {
            query.Add($"formato={Uri.EscapeDataString(request.Formato.Trim())}");
        }

        if (!string.IsNullOrWhiteSpace(request.MensagemRodape))
        {
            query.Add($"mensagem_rodape={Uri.EscapeDataString(request.MensagemRodape.Trim())}");
        }

        if (request.Canhoto.HasValue)
        {
            query.Add($"canhoto={request.Canhoto.Value.ToString().ToLowerInvariant()}");
        }

        return query.Count == 0
            ? $"/nfe/{documentoId}/pdf"
            : $"/nfe/{documentoId}/pdf?{string.Join("&", query)}";
    }

    private static string GuessMediaType(NFeDownloadDocumentoTipo tipo)
        => tipo is NFeDownloadDocumentoTipo.DanfePdf or NFeDownloadDocumentoTipo.CancelamentoPdf or NFeDownloadDocumentoTipo.CartaCorrecaoPdf
            ? "application/pdf"
            : "application/xml";

    private static bool IsTextLikeContent(string mediaType, NFeDownloadDocumentoTipo tipo)
        => tipo is NFeDownloadDocumentoTipo.XmlProcessado
            or NFeDownloadDocumentoTipo.XmlNota
            or NFeDownloadDocumentoTipo.XmlProtocolo
            or NFeDownloadDocumentoTipo.CancelamentoXml
            or NFeDownloadDocumentoTipo.CartaCorrecaoXml
            || mediaType.Contains("xml", StringComparison.OrdinalIgnoreCase)
            || mediaType.StartsWith("text/", StringComparison.OrdinalIgnoreCase)
            || mediaType.Contains("json", StringComparison.OrdinalIgnoreCase);

    private static string BuildFileName(ResolvedNFeDocument resolved, NFeDownloadDocumentoTipo tipo, string mediaType)
    {
        var suffix = tipo switch
        {
            NFeDownloadDocumentoTipo.DanfePdf => "danfe",
            NFeDownloadDocumentoTipo.XmlProcessado => "nfe-proc",
            NFeDownloadDocumentoTipo.XmlNota => "nfe-nota",
            NFeDownloadDocumentoTipo.XmlProtocolo => "protocolo",
            NFeDownloadDocumentoTipo.CancelamentoPdf => "cancelamento",
            NFeDownloadDocumentoTipo.CancelamentoXml => "cancelamento",
            NFeDownloadDocumentoTipo.CartaCorrecaoPdf => "carta-correcao",
            NFeDownloadDocumentoTipo.CartaCorrecaoXml => "carta-correcao",
            _ => "documento"
        };
        var extension = mediaType.Contains("pdf", StringComparison.OrdinalIgnoreCase) ? "pdf" : "xml";
        var safeReference = new string(resolved.Referencia.Where(ch => char.IsLetterOrDigit(ch) || ch is '-' or '_').ToArray());
        return $"{safeReference}-{suffix}.{extension}";
    }

    private async Task EnsureProviderReadyAsync(HttpClient client)
    {
        await EnsureEmpresaAsync(client);
        await EnsureCertificateAsync(client);
        await EnsureNfeConfigurationAsync(client);
    }

    private string ResolveBaseUrl()
        => NormalizeBaseUrl(
            Context.BaseUrl,
            Context.Ambiente == AmbienteFiscal.Homologacao
                ? DefaultSandboxBaseUrl
                : DefaultProductionBaseUrl);

    private async Task EnsureEmpresaAsync(HttpClient client)
    {
        var payload = BuildEmpresaPayload();
        using var response = await client.PutAsync($"/empresas/{Context.Cnpj}", JsonContent(payload));
        var responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        if (!IsNotFoundResponse(response.StatusCode, responseBody))
        {
            throw new AppException($"Nao foi possivel sincronizar a empresa na Nuvem Fiscal: {Summarize(responseBody)}");
        }

        using var createResponse = await client.PostAsync("/empresas", JsonContent(payload));
        var createBody = await createResponse.Content.ReadAsStringAsync();
        if (!createResponse.IsSuccessStatusCode)
        {
            throw new AppException($"Nao foi possivel cadastrar a empresa na Nuvem Fiscal: {Summarize(createBody)}");
        }
    }

    private async Task EnsureCertificateAsync(HttpClient client)
    {
        var path = Context.CertificadoDigitalCaminho;
        var password = Context.CertificadoDigitalSenha;
        if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(password))
        {
            throw new AppException("Para usar a Nuvem Fiscal, configure na empresa o certificado digital local e a senha para sincronizar o emitente com o provider.");
        }

        using var localCertificate = nfeCertificateService.LoadFromPath(path, password);
        var localThumbprint = NormalizeThumbprint(localCertificate.Thumbprint);
        var shouldUpload = true;

        using var certificateResponse = await client.GetAsync($"/empresas/{Context.Cnpj}/certificado");
        var certificateBody = await certificateResponse.Content.ReadAsStringAsync();
        if (certificateResponse.IsSuccessStatusCode)
        {
            using var certificateJson = JsonDocument.Parse(certificateBody);
            var remoteThumbprint = NormalizeThumbprint(ReadString(certificateJson.RootElement, "thumbprint"));
            shouldUpload = string.IsNullOrWhiteSpace(remoteThumbprint)
                || !string.Equals(remoteThumbprint, localThumbprint, StringComparison.OrdinalIgnoreCase);
        }
        else if (!IsNotFoundResponse(certificateResponse.StatusCode, certificateBody))
        {
            throw new AppException($"Nao foi possivel consultar o certificado da empresa na Nuvem Fiscal: {Summarize(certificateBody)}");
        }

        if (!shouldUpload)
        {
            return;
        }

        var bytes = await File.ReadAllBytesAsync(path);
        using var uploadResponse = await client.PutAsync(
            $"/empresas/{Context.Cnpj}/certificado",
            JsonContent(new
            {
                certificado = Convert.ToBase64String(bytes),
                password
            }));
        var uploadBody = await uploadResponse.Content.ReadAsStringAsync();
        if (!uploadResponse.IsSuccessStatusCode)
        {
            throw new AppException($"Nao foi possivel enviar o certificado para a Nuvem Fiscal: {Summarize(uploadBody)}");
        }
    }

    private async Task EnsureNfeConfigurationAsync(HttpClient client)
    {
        using var response = await client.PutAsync(
            $"/empresas/{Context.Cnpj}/nfe",
            JsonContent(new
            {
                CRT = int.Parse(ResolveCrt(Context.RegimeTributario)),
                ambiente = AmbienteLiteral(Context.Ambiente)
            }));
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new AppException($"Nao foi possivel configurar o ambiente NF-e da empresa na Nuvem Fiscal: {Summarize(responseBody)}");
        }
    }

    private static string FormatNFeEmissionDate(DateTime dataUtc)
    {
        DateTime utc;

        switch (dataUtc.Kind)
        {
            case DateTimeKind.Utc:
                utc = dataUtc;
                break;

            case DateTimeKind.Local:
                utc = dataUtc.ToUniversalTime();
                break;

            default:
                // O contrato do DTO é DataEmissaoUtc.
                // Portanto DateTime sem Kind é tratado como UTC.
                utc = DateTime.SpecifyKind(
                    dataUtc,
                    DateTimeKind.Utc);
                break;
        }

        var brasilia = GetBrazilTimeZone();

        var local =
            TimeZoneInfo.ConvertTimeFromUtc(
                utc,
                brasilia);

        var offset =
            brasilia.GetUtcOffset(local);

        var dateTimeOffset =
            new DateTimeOffset(
                DateTime.SpecifyKind(
                    local,
                    DateTimeKind.Unspecified),
                offset);

        return dateTimeOffset.ToString(
            "yyyy-MM-dd'T'HH:mm:sszzz",
            CultureInfo.InvariantCulture);
    }

    private static int ResolveIdDest(
    NFeEnderecoRequest emitente,
    NFeEnderecoRequest? destinatario)
    {
        if (destinatario is null)
        {
            throw new AppException(
                "O destinatario precisa possuir endereco para determinar o tipo de operacao.");
        }

        var ufEmitente =
            NormalizeUf(emitente.Uf);

        var ufDestinatario =
            NormalizeUf(destinatario.Uf);

        if (string.IsNullOrWhiteSpace(ufEmitente))
        {
            throw new AppException(
                "A UF do emitente nao foi informada.");
        }

        if (string.IsNullOrWhiteSpace(ufDestinatario))
        {
            throw new AppException(
                "A UF do destinatario nao foi informada.");
        }

        if (!IsValidUf(ufEmitente))
        {
            throw new AppException(
                $"UF do emitente invalida: {ufEmitente}.");
        }

        if (!IsValidUf(ufDestinatario))
        {
            throw new AppException(
                $"UF do destinatario invalida: {ufDestinatario}.");
        }

        return string.Equals(
            ufEmitente,
            ufDestinatario,
            StringComparison.OrdinalIgnoreCase)
            ? 1
            : 2;
    }

    private static string NormalizeUf(string? value)
        => value?.Trim().ToUpperInvariant() ?? string.Empty;

    private static bool IsValidUf(string uf)
        => uf switch
        {
            "AC" or "AL" or "AP" or "AM" or
            "BA" or "CE" or "DF" or "ES" or
            "GO" or "MA" or "MT" or "MS" or
            "MG" or "PA" or "PB" or "PR" or
            "PE" or "PI" or "RJ" or "RN" or
            "RS" or "RO" or "RR" or "SC" or
            "SP" or "SE" or "TO" => true,

            _ => false
        };

    private static TimeZoneInfo GetBrazilTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById(
                "America/Sao_Paulo");
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.FindSystemTimeZoneById(
                "E. South America Standard Time");
        }
        catch (InvalidTimeZoneException)
        {
            return TimeZoneInfo.Utc;
        }
    }

    private object BuildEmpresaPayload()
    {
        if (Context.EnderecoEmitente is null)
        {
            throw new AppException("Preencha logradouro, numero, bairro, cidade, UF, CEP e codigo IBGE da empresa antes de sincronizar a Nuvem Fiscal.");
        }

        if (string.IsNullOrWhiteSpace(Context.EmailFiscal))
        {
            throw new AppException("Informe o e-mail fiscal da empresa antes de sincronizar a Nuvem Fiscal.");
        }

        return new
        {
            cpf_cnpj = Context.Cnpj,
            inscricao_estadual = Context.InscricaoEstadualIsento ? "ISENTO" : Context.InscricaoEstadual,
            inscricao_municipal = Context.InscricaoMunicipal,
            nome_razao_social = Context.EmpresaNome,
            nome_fantasia = Context.NomeFantasia,
            fone = OnlyDigits(Context.Telefone),
            email = Context.EmailFiscal,
            endereco = new
            {
                logradouro = Context.EnderecoEmitente.Logradouro,
                numero = Context.EnderecoEmitente.Numero,
                complemento = Context.EnderecoEmitente.Complemento,
                bairro = Context.EnderecoEmitente.Bairro,
                codigo_municipio = Context.EnderecoEmitente.CodigoMunicipioIbge,
                cidade = Context.EnderecoEmitente.Cidade,
                uf = Context.EnderecoEmitente.Uf,
                codigo_pais = "1058",
                pais = "Brasil",
                cep = OnlyDigits(Context.EnderecoEmitente.Cep)
            }
        };
    }

    private object BuildProviderPayload(
    NFeRequest document)
    {
        ValidateDocumentForEmission(document);

        var idDest =
            ResolveIdDest(
                document.Emitente.Endereco,
                document.Destinatario.Endereco);

        return new
        {
            ambiente =
                AmbienteLiteral(document.Ambiente),

            referencia =
                document.Referencia.Trim(),

            infNFe = new
            {
                versao = "4.00",

                ide = new
                {
                    cUF =
                        ResolveUfCode(
                            document.Emitente.Endereco.Uf),

                    natOp =
                        document.NaturezaOperacao,

                    mod = 55,

                    serie =
                        document.Serie,

                    nNF =
                        document.Numero,

                    dhEmi =
                        FormatNFeEmissionDate(
                            document.DataEmissaoUtc),

                    tpNF = 1,

                    idDest,

                    cMunFG =
                        document.Emitente.Endereco.CodigoMunicipioIbge,

                    tpImp = 1,

                    tpEmis = 1,

                    finNFe = 1,

                    indFinal = 1,

                    indPres = 1,

                    procEmi = 0,

                    verProc = "PDV-V1/1.0",

                    // O leiaute da NF-e prevê tpAmb dentro do ide.
                    tpAmb =
                        document.Ambiente == AmbienteFiscal.Homologacao
                            ? 2
                            : 1
                },

                emit = new
                {
                    CNPJ =
                        OnlyDigits(document.Emitente.Cnpj),

                    xNome =
                        document.Emitente.RazaoSocial,

                    xFant =
                        document.Emitente.NomeFantasia,

                    enderEmit =
                        BuildAddressNode(
                            document.Emitente.Endereco),

                    IE =
                        document.Emitente.InscricaoEstadualIsento
                            ? "ISENTO"
                            : document.Emitente.InscricaoEstadual,

                    IM =
                        document.Emitente.InscricaoMunicipal,

                    CNAE =
                        document.Emitente.CnaePrincipal,

                    CRT =
                        ResolveCrt(
                            document.Emitente.RegimeTributario)
                },

                dest =
                    BuildDestNode(
                        document.Destinatario),

                det =
                    document.Itens
                        .Select(
                            (item, index) =>
                                BuildItemNode(
                                    item,
                                    index + 1,
                                    document.Emitente.RegimeTributario))
                        .ToArray(),

                total =
                    BuildTotalNode(document),

                transp = new
                {
                    modFrete = 9
                },

                pag = new
                {
                    detPag =
                        document.Pagamentos
                            .Select(item =>
                            {
                                var code =
                                    ResolvePaymentCode(
                                        item.FormaPagamento);

                                return new
                                {
                                    tPag = code,

                                    xPag =
                                        code == "99"
                                            ? item.FormaPagamento?.Trim()
                                            : null,

                                    vPag =
                                        RoundMoney(item.ValorPago)
                                };
                            })
                            .ToArray()
                },

                infAdic =
                    string.IsNullOrWhiteSpace(
                        document.ObservacoesComplementares)
                        ? null
                        : new
                        {
                            infCpl =
                                document.ObservacoesComplementares.Trim()
                        }
            }
        };
    }

    private void ValidateDocumentForEmission(
   NFeRequest document)
    {
        if (string.IsNullOrWhiteSpace(document.Referencia))
        {
            throw new AppException(
                "A NF-e precisa possuir uma referencia para garantir idempotencia.");
        }

        if (document.Referencia.Trim().Length > 50)
        {
            throw new AppException(
                "A referencia da NF-e nao pode ultrapassar 50 caracteres.");
        }

        if (document.Ambiente != Context.Ambiente)
        {
            throw new AppException(
                "O ambiente da NF-e nao corresponde ao ambiente configurado no provider.");
        }

        if (document.Serie < 0 || document.Serie > 999)
        {
            throw new AppException(
                "A serie da NF-e deve estar entre 0 e 999.");
        }

        if (document.Numero <= 0)
        {
            throw new AppException(
                "O numero da NF-e deve ser maior que zero.");
        }

        if (string.IsNullOrWhiteSpace(
            document.Emitente.Endereco.Uf))
        {
            throw new AppException(
                "A UF do emitente e obrigatoria.");
        }

        if (document.Destinatario.Endereco is null)
        {
            throw new AppException(
                "O endereco do destinatario e obrigatorio para esta emissao.");
        }

        if (document.Itens is null ||
            document.Itens.Count == 0)
        {
            throw new AppException(
                "A NF-e precisa possuir pelo menos um item.");
        }

        if (document.Pagamentos is null ||
            document.Pagamentos.Count == 0)
        {
            throw new AppException(
                "A NF-e precisa possuir pelo menos um pagamento.");
        }

        foreach (var item in document.Itens)
        {
            if (string.IsNullOrWhiteSpace(item.Cfop))
            {
                throw new AppException(
                    $"O produto '{item.Nome}' nao possui CFOP.");
            }

            var cfop = OnlyDigits(item.Cfop);
            if (cfop is null || cfop.Length != 4)
            {
                throw new AppException(
                    $"CFOP invalido no produto '{item.Nome}': deve conter exatamente 4 digitos numericos.");
            }

            if (string.IsNullOrWhiteSpace(item.Ncm))
            {
                throw new AppException(
                    $"O produto '{item.Nome}' nao possui NCM.");
            }

            var ncm = OnlyDigits(item.Ncm);
            if (ncm is null || ncm.Length != 8)
            {
                throw new AppException(
                    $"NCM invalido no produto '{item.Nome}': deve conter exatamente 8 digitos numericos.");
            }

            if (!string.IsNullOrWhiteSpace(item.Cest))
            {
                var cest = OnlyDigits(item.Cest);
                if (cest is null || cest.Length != 7)
                {
                    throw new AppException(
                        $"CEST invalido no produto '{item.Nome}': deve conter exatamente 7 digitos numericos.");
                }
            }

            if (string.IsNullOrWhiteSpace(
                item.UnidadeComercial))
            {
                throw new AppException(
                    $"O produto '{item.Nome}' nao possui unidade comercial.");
            }
        }

        // A SEFAZ exige vNF == soma de vPag (grupo <pag> do layout 4.00).
        // Validar aqui evita descobrir a divergencia so depois da chamada
        // externa, com o cliente ja esperando a nota na frente do caixa.
        var totalPagamentos = RoundMoney(document.Pagamentos.Sum(p => p.ValorPago));
        var valorTotalDocumento = RoundMoney(document.ValorTotal);
        if (totalPagamentos != valorTotalDocumento)
        {
            throw new AppException(
                $"A soma dos pagamentos (R$ {totalPagamentos.ToString(CultureInfo.InvariantCulture)}) nao corresponde " +
                $"ao valor total da NF-e (R$ {valorTotalDocumento.ToString(CultureInfo.InvariantCulture)}).");
        }
    }

    private static object BuildAddressNode(NFeEnderecoRequest address)
        => new
        {
            xLgr = address.Logradouro,
            nro = address.Numero,
            xCpl = address.Complemento,
            xBairro = address.Bairro,
            cMun = address.CodigoMunicipioIbge,
            xMun = address.Cidade,
            UF = address.Uf,
            CEP = OnlyDigits(address.Cep),
            cPais = "1058",
            xPais = "BRASIL",
            fone = OnlyDigits(address.Telefone)
        };

    private static object BuildDestNode(NFeDestinatarioRequest destinatario)
    {
        var digits = OnlyDigits(destinatario.Documento);
        return new
        {
            CNPJ = digits?.Length == 14 ? digits : null,
            CPF = digits?.Length == 11 ? digits : null,
            xNome = destinatario.Nome,
            enderDest = destinatario.Endereco is null ? null : BuildAddressNode(destinatario.Endereco),
            indIEDest = 9,
            email = destinatario.Email
        };
    }

    private static object BuildItemNode(
    NFeItemRequest item,
    int index,
    EmpresaRegimeTributario regime)
    {
        if (item.Quantidade <= 0m)
        {
            throw new AppException(
                $"Quantidade invalida no produto '{item.Nome}'.");
        }

        if (item.ValorUnitario < 0m)
        {
            throw new AppException(
                $"Valor unitario invalido no produto '{item.Nome}'.");
        }

        if (item.Total < 0m)
        {
            throw new AppException(
                $"Total invalido no produto '{item.Nome}'.");
        }

        var baseCalculo =
            RoundMoney(item.Total);

        var aliquotaPis =
            item.Impostos.AliquotaPis ?? 0m;

        var aliquotaCofins =
            item.Impostos.AliquotaCofins ?? 0m;

        var valorPis =
            CalculateContribution(
                item.Impostos.CstPis,
                baseCalculo,
                aliquotaPis,
                "PIS",
                item.Nome);

        var valorCofins =
            CalculateContribution(
                item.Impostos.CstCofins,
                baseCalculo,
                aliquotaCofins,
                "COFINS",
                item.Nome);

        // GTIN/EAN so deve ser enviado se for um GTIN valido de verdade
        // (tamanho 8/12/13/14 + digito verificador correto). Mandar um
        // codigo interno qualquer como se fosse GTIN gera rejeicao na
        // SEFAZ ou, pior, aceitacao de um GTIN que nao corresponde ao
        // produto. Quando nao houver GTIN valido, o layout exige "SEM GTIN".
        var codigoBarras = ResolveGtin(item.CodigoBarras);

        return new
        {
            nItem = index,

            prod = new
            {
                cProd = item.CodigoProduto,
                cEAN = codigoBarras,
                xProd = item.Nome,
                NCM = OnlyDigits(item.Ncm),
                CEST = OnlyDigits(item.Cest),
                CFOP = OnlyDigits(item.Cfop),
                uCom = item.UnidadeComercial,
                qCom = item.Quantidade,
                vUnCom = item.ValorUnitario,
                vProd = RoundMoney(
                    item.Quantidade * item.ValorUnitario),
                cEANTrib = codigoBarras,
                uTrib = item.UnidadeTributavel,
                qTrib = item.Quantidade,
                vUnTrib = item.ValorUnitario,
                vDesc = item.Desconto > 0m
                ? (decimal?)item.Desconto
                : null,
                indTot = 1
            },

            imposto = new
            {
                ICMS =
                    BuildIcmsNode(
                        item,
                        regime),

                PIS =
                    BuildPisNode(
                        item.Impostos.CstPis,
                        baseCalculo,
                        aliquotaPis,
                        valorPis),

                COFINS =
                    BuildCofinsNode(
                        item.Impostos.CstCofins,
                        baseCalculo,
                        aliquotaCofins,
                        valorCofins)
            }
        };
    }

    private static string ResolveGtin(string? codigoBarras)
    {
        const string semGtin = "SEM GTIN";

        var digits = OnlyDigits(codigoBarras);
        if (string.IsNullOrWhiteSpace(digits))
        {
            return semGtin;
        }

        if (digits.Length is not (8 or 12 or 13 or 14))
        {
            return semGtin;
        }

        return IsValidGtinCheckDigit(digits) ? digits : semGtin;
    }

    private static bool IsValidGtinCheckDigit(string digits)
    {
        var checkDigit = digits[^1] - '0';
        var sum = 0;
        var useWeightThree = true;

        for (var i = digits.Length - 2; i >= 0; i--)
        {
            var digit = digits[i] - '0';
            sum += digit * (useWeightThree ? 3 : 1);
            useWeightThree = !useWeightThree;
        }

        var calculatedCheckDigit = (10 - (sum % 10)) % 10;
        return calculatedCheckDigit == checkDigit;
    }

    private static decimal CalculateContribution(
        string? cstValue,
        decimal baseCalculo,
        decimal aliquota,
        string tributo,
        string produto)
    {
        var cst =
            NormalizeRequiredTaxCode(
                cstValue,
                $"CST {tributo}",
                produto,
                2);

        if (cst is
            "04" or
            "05" or
            "06" or
            "07" or
            "08" or
            "09")
        {
            if (aliquota != 0m)
            {
                throw new AppException(
                    $"CST {tributo} {cst} nao aceita aliquota percentual no produto '{produto}'.");
            }

            return 0m;
        }

        if (cst == "03")
        {
            throw new AppException(
                $"CST {tributo} 03 exige tributacao por quantidade no produto '{produto}', " +
                "mas o DTO atual nao possui os campos necessarios.");
        }

        RequireContributionRate(
            cst,
            aliquota,
            tributo);

        return RoundMoney(
            baseCalculo * aliquota / 100m);
    }

    private static object BuildIcmsNode(
    NFeItemRequest item,
    EmpresaRegimeTributario regime)
    {
        var origemFiscal = ResolveOrigem(item.Impostos.OrigemFiscal);

        var simples = regime is
            EmpresaRegimeTributario.SimplesNacional
            or EmpresaRegimeTributario.SimplesExcessoSublimite;

        if (simples)
        {
            return item.Impostos.Csosn switch
            {
                "102" or "103" or "300" or "400" => new
                {
                    ICMSSN102 = new
                    {
                        orig = origemFiscal,
                        CSOSN = item.Impostos.Csosn
                    }
                },

                "500" => new
                {
                    ICMSSN500 = new
                    {
                        orig = origemFiscal,
                        CSOSN = "500",
                        vBCSTRet = 0m,
                        pST = 0m,
                        vICMSSubstituto = 0m,
                        vICMSSTRet = 0m
                    }
                },

                _ => new
                {
                    ICMSSN102 = new
                    {
                        orig = origemFiscal,
                        CSOSN = "102"
                    }
                }
            };
        }

        var baseCalculo = item.Total;
        var aliquota = item.Impostos.AliquotaIcms ?? 0m;
        var valorIcms = Math.Round(
            baseCalculo * aliquota / 100m,
            2,
            MidpointRounding.AwayFromZero);

        return item.Impostos.CstIcms switch
        {
            "00" => new
            {
                ICMS00 = new
                {
                    orig = origemFiscal,
                    CST = "00",
                    modBC = 3,
                    vBC = baseCalculo,
                    pICMS = aliquota,
                    vICMS = valorIcms
                }
            },

            "40" or "41" => new
            {
                ICMS40 = new
                {
                    orig = origemFiscal,
                    CST = item.Impostos.CstIcms
                }
            },

            _ => new
            {
                ICMS40 = new
                {
                    orig = origemFiscal,
                    CST = "40"
                }
            }
        };
    }

    private static string NormalizeRequiredTaxCode(
    string? value,
    string fieldName,
    string produto,
    int expectedLength)
    {
        var normalized =
            value?.Trim();

        if (string.IsNullOrWhiteSpace(normalized))
        {
            throw new AppException(
                $"O produto '{produto}' nao possui {fieldName} configurado.");
        }

        if (normalized.Length != expectedLength ||
            !normalized.All(char.IsDigit))
        {
            throw new AppException(
                $"{fieldName} '{normalized}' invalido no produto '{produto}'.");
        }

        return normalized;
    }

    private static decimal RequireNonNegative(
        decimal value,
        string fieldName)
    {
        if (value < 0m)
        {
            throw new AppException(
                $"{fieldName} nao pode ser negativo.");
        }

        return RoundMoney(value);
    }

    private static decimal RoundMoney(decimal value)
        => Math.Round(
            value,
            2,
            MidpointRounding.AwayFromZero);

    private static object BuildIcms00(
        string origem,
        decimal baseCalculo,
        decimal aliquota,
        string produto)
    {
        if (aliquota <= 0m)
        {
            throw new AppException(
                $"CST 00 exige aliquota ICMS maior que zero no produto '{produto}'.");
        }

        var valor =
            RoundMoney(
                baseCalculo * aliquota / 100m);

        return new
        {
            ICMS00 = new
            {
                orig = origem,
                CST = "00",
                modBC = 3,
                vBC = baseCalculo,
                pICMS = aliquota,
                vICMS = valor
            }
        };
    }

    private static object BuildIcms90(
        string origem,
        decimal baseCalculo,
        decimal aliquota,
        string produto)
    {
        if (aliquota <= 0m)
        {
            throw new AppException(
                $"CST 90 exige aliquota ICMS configurada no produto '{produto}'.");
        }

        var valor =
            RoundMoney(
                baseCalculo * aliquota / 100m);

        return new
        {
            ICMS90 = new
            {
                orig = origem,
                CST = "90",
                modBC = 3,
                vBC = baseCalculo,
                pICMS = aliquota,
                vICMS = valor
            }
        };
    }

    private static object BuildPisNode(
    string? cstPis,
    decimal baseCalculo,
    decimal aliquota,
    decimal valorPis)
    {
        var cst =
            NormalizeRequiredTaxCode(
                cstPis,
                "CST PIS",
                "item",
                2);

        switch (cst)
        {
            case "01":
            case "02":
                RequireContributionRate(
                    cst,
                    aliquota,
                    "PIS");

                return new
                {
                    PISAliq = new
                    {
                        CST = cst,
                        vBC = baseCalculo,
                        pPIS = aliquota,
                        vPIS = valorPis
                    }
                };

            case "04":
            case "05":
            case "06":
            case "07":
            case "08":
            case "09":
                if (aliquota != 0m)
                {
                    throw new AppException(
                        $"CST PIS {cst} nao pode possuir aliquota percentual neste DTO.");
                }

                return new
                {
                    PISNT = new
                    {
                        CST = cst
                    }
                };

            case "03":
                throw new AppException(
                    "CST PIS 03 exige qBCProd/vAliqProd. " +
                    "O DTO atual nao possui esses campos.");

            case "49":
            case "50":
            case "51":
            case "52":
            case "53":
            case "54":
            case "55":
            case "56":
            case "60":
            case "61":
            case "62":
            case "63":
            case "64":
            case "65":
            case "66":
            case "67":
            case "70":
            case "71":
            case "72":
            case "73":
            case "74":
            case "75":
            case "98":
            case "99":
                RequireContributionRate(
                    cst,
                    aliquota,
                    "PIS");

                return new
                {
                    PISOutr = new
                    {
                        CST = cst,
                        vBC = baseCalculo,
                        pPIS = aliquota,
                        vPIS = valorPis
                    }
                };

            default:
                throw new AppException(
                    $"CST PIS '{cst}' nao possui mapeamento seguro no emissor.");
        }
    }

    private static object BuildCofinsNode(
    string? cstCofins,
    decimal baseCalculo,
    decimal aliquota,
    decimal valorCofins)
    {
        var cst =
            NormalizeRequiredTaxCode(
                cstCofins,
                "CST COFINS",
                "item",
                2);

        switch (cst)
        {
            case "01":
            case "02":
                RequireContributionRate(
                    cst,
                    aliquota,
                    "COFINS");

                return new
                {
                    COFINSAliq = new
                    {
                        CST = cst,
                        vBC = baseCalculo,
                        pCOFINS = aliquota,
                        vCOFINS = valorCofins
                    }
                };

            case "04":
            case "05":
            case "06":
            case "07":
            case "08":
            case "09":
                if (aliquota != 0m)
                {
                    throw new AppException(
                        $"CST COFINS {cst} nao pode possuir aliquota percentual neste DTO.");
                }

                return new
                {
                    COFINSNT = new
                    {
                        CST = cst
                    }
                };

            case "03":
                throw new AppException(
                    "CST COFINS 03 exige qBCProd/vAliqProd. " +
                    "O DTO atual nao possui esses campos.");

            case "49":
            case "50":
            case "51":
            case "52":
            case "53":
            case "54":
            case "55":
            case "56":
            case "60":
            case "61":
            case "62":
            case "63":
            case "64":
            case "65":
            case "66":
            case "67":
            case "70":
            case "71":
            case "72":
            case "73":
            case "74":
            case "75":
            case "98":
            case "99":
                RequireContributionRate(
                    cst,
                    aliquota,
                    "COFINS");

                return new
                {
                    COFINSOutr = new
                    {
                        CST = cst,
                        vBC = baseCalculo,
                        pCOFINS = aliquota,
                        vCOFINS = valorCofins
                    }
                };

            default:
                throw new AppException(
                    $"CST COFINS '{cst}' nao possui mapeamento seguro no emissor.");
        }
    }

    private static void RequireContributionRate(
        string cst,
        decimal aliquota,
        string tributo)
    {
        if (aliquota < 0m || aliquota > 100m)
        {
            throw new AppException(
                $"Aliquota {tributo} invalida para CST {cst}: {aliquota}.");
        }

        if (aliquota <= 0m)
        {
            throw new AppException(
                $"CST {cst} de {tributo} exige aliquota configurada.");
        }
    }

    private static object BuildTotalNode(
    NFeRequest document)
    {
        var totalPis = 0m;
        var totalCofins = 0m;
        var totalIcmsBase = 0m;
        var totalIcms = 0m;

        foreach (var item in document.Itens)
        {
            var baseCalculo =
                RoundMoney(item.Total);

            totalPis +=
                CalculateContribution(
                    item.Impostos.CstPis,
                    baseCalculo,
                    item.Impostos.AliquotaPis ?? 0m,
                    "PIS",
                    item.Nome);

            totalCofins +=
                CalculateContribution(
                    item.Impostos.CstCofins,
                    baseCalculo,
                    item.Impostos.AliquotaCofins ?? 0m,
                    "COFINS",
                    item.Nome);

            var simples =
                document.Emitente.RegimeTributario
                    is EmpresaRegimeTributario.SimplesNacional
                    or EmpresaRegimeTributario.SimplesExcessoSublimite;

            if (!simples)
            {
                var cst =
                    NormalizeRequiredTaxCode(
                        item.Impostos.CstIcms,
                        "CST ICMS",
                        item.Nome,
                        2);

                switch (cst)
                {
                    case "00":
                    case "90":
                        var aliquota =
                            item.Impostos.AliquotaIcms ?? 0m;

                        if (aliquota <= 0m)
                        {
                            throw new AppException(
                                $"CST ICMS {cst} exige aliquota configurada no produto '{item.Nome}'.");
                        }

                        totalIcmsBase += baseCalculo;

                        totalIcms +=
                            RoundMoney(
                                baseCalculo * aliquota / 100m);

                        break;

                    case "40":
                    case "41":
                    case "50":
                        break;

                    default:
                        // Faz a mesma validação fiscal do item.
                        _ = BuildIcmsNode(
                            item,
                            document.Emitente.RegimeTributario);

                        break;
                }
            }
        }

        return new
        {
            ICMSTot = new
            {
                vBC = RoundMoney(totalIcmsBase),
                vICMS = RoundMoney(totalIcms),
                vICMSDeson = 0m,
                vFCP = 0m,
                vBCST = 0m,
                vST = 0m,
                vFCPST = 0m,
                vFCPSTRet = 0m,
                vProd = RoundMoney(document.ValorProdutos),
                vFrete = 0m,
                vSeg = 0m,
                vDesc = RoundMoney(document.ValorDesconto),
                vII = 0m,
                vIPI = 0m,
                vIPIDevol = 0m,
                vPIS = RoundMoney(totalPis),
                vCOFINS = RoundMoney(totalCofins),
                vOutro = 0m,
                vNF = RoundMoney(document.ValorTotal)
            }
        };
    }

    private static string ResolveCrt(EmpresaRegimeTributario regime)
        => regime switch
        {
            EmpresaRegimeTributario.SimplesNacional => "1",
            EmpresaRegimeTributario.SimplesExcessoSublimite => "2",
            _ => "3"
        };

    private static string ResolveOrigem(string? origemFiscal)
    {
        var digits =
            OnlyDigits(origemFiscal);

        if (string.IsNullOrWhiteSpace(digits))
        {
            return "0";
        }

        if (digits.Length != 1 ||
            digits[0] < '0' ||
            digits[0] > '8')
        {
            throw new AppException(
                $"Origem fiscal invalida: '{origemFiscal}'.");
        }

        return digits;
    }

    private static string ResolvePaymentCode(string formaPagamento)
        => formaPagamento.ToLowerInvariant() switch
        {
            "dinheiro" => "01",
            "cartaocredito" => "03",
            "cartaodebito" => "04",
            "pix" => "17",
            "voucher" => "99",
            _ => "99"
        };

    private static int ResolveUfCode(string uf)
        => uf.ToUpperInvariant() switch
        {
            "RO" => 11,
            "AC" => 12,
            "AM" => 13,
            "RR" => 14,
            "PA" => 15,
            "AP" => 16,
            "TO" => 17,
            "MA" => 21,
            "PI" => 22,
            "CE" => 23,
            "RN" => 24,
            "PB" => 25,
            "PE" => 26,
            "AL" => 27,
            "SE" => 28,
            "BA" => 29,
            "MG" => 31,
            "ES" => 32,
            "RJ" => 33,
            "SP" => 35,
            "PR" => 41,
            "SC" => 42,
            "RS" => 43,
            "MS" => 50,
            "MT" => 51,
            "GO" => 52,
            "DF" => 53,
            _ => 35
        };

    private static FiscalDocumentoStatus MapStatus(
    string? status,
    int? code)
    {
        var normalized =
            status?
                .Trim()
                .ToLowerInvariant();

        return normalized switch
        {
            "autorizado" or
            "autorizada" or
            "concluido" or
            "concluida" =>
                FiscalDocumentoStatus.Autorizada,

            "cancelado" or
            "cancelada" =>
                FiscalDocumentoStatus.Cancelada,

            "rejeitado" or
            "rejeitada" or
            "denegado" or
            "denegada" or
            "erro" or
            "error" =>
                FiscalDocumentoStatus.Rejeitada,

            "pendente" or
            "processando" or
            "processando_sefaz" or
            "em_processamento" =>
                FiscalDocumentoStatus.Pendente,

            _ =>
                MapByCode(code)
        };
    }

    private static FiscalDocumentoStatus MapByCode(
        int? code)
    {
        return code switch
        {
            // Autorização normal
            100 or 150 =>
                FiscalDocumentoStatus.Autorizada,

            // Processamento ainda não concluído
            103 or 104 or 105 or 106 or
            107 or 108 or 109 =>
                FiscalDocumentoStatus.Pendente,

            // Uso denegado
            110 =>
                FiscalDocumentoStatus.Rejeitada,

            // Códigos de evento não devem transformar uma NF-e
            // em autorizada automaticamente.
            128 or 135 or 136 =>
                FiscalDocumentoStatus.Pendente,

            // Cancelamento
            101 or 151 or 155 =>
                FiscalDocumentoStatus.Cancelada,

            // Rejeições SEFAZ
            >= 200 =>
                FiscalDocumentoStatus.Rejeitada,

            _ =>
                FiscalDocumentoStatus.Pendente
        };
    }

    private static string? ReadString(JsonElement node, string propertyName)
        => node.TryGetProperty(propertyName, out var value) && value.ValueKind != JsonValueKind.Null
            ? value.GetString()
            : null;

    private static int? ReadNullableInt(JsonElement node, string propertyName)
        => node.TryGetProperty(propertyName, out var value) && value.TryGetInt32(out var intValue)
            ? intValue
            : null;

    private static string? ReadNestedString(JsonElement node, string parent, string child)
        => node.TryGetProperty(parent, out var parentNode) ? ReadString(parentNode, child) : null;

    private static int? ReadNestedNullableInt(JsonElement node, string parent, string child)
        => node.TryGetProperty(parent, out var parentNode) ? ReadNullableInt(parentNode, child) : null;

    private static DateTime? ParseDate(
    string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return DateTimeOffset.TryParse(
            value,
            CultureInfo.InvariantCulture,
            DateTimeStyles.RoundtripKind,
            out var parsed)
            ? parsed.UtcDateTime
            : null;
    }

    private static bool IsNotFoundResponse(HttpStatusCode statusCode, string? responseBody)
    {
        if (statusCode == HttpStatusCode.NotFound)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(responseBody))
        {
            return false;
        }

        return responseBody.Contains("notfound", StringComparison.OrdinalIgnoreCase)
            || responseBody.Contains("not found", StringComparison.OrdinalIgnoreCase)
            || responseBody.Contains("CompanyNotFound", StringComparison.OrdinalIgnoreCase)
            || responseBody.Contains("CertificateNotFound", StringComparison.OrdinalIgnoreCase);
    }

    private static string? NormalizeThumbprint(string? value)
        => string.IsNullOrWhiteSpace(value)
            ? null
            : new string(value.Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();

    private static string Summarize(string? responseBody)
        => string.IsNullOrWhiteSpace(responseBody)
            ? "retorno vazio"
            : responseBody.Length <= 400 ? responseBody : responseBody[..400];
}
