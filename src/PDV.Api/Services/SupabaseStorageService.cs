using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using PDV.Api.Infrastructure;

namespace PDV.Api.Services;

public class SupabaseStorageService
{
    private readonly HttpClient _http;
    private readonly SupabaseStorageOptions _options;

    public SupabaseStorageService(HttpClient http, IOptions<SupabaseStorageOptions> options)
    {
        _options = options.Value;
        _http = http;
        _http.BaseAddress = new Uri(_options.Url.TrimEnd('/') + "/");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.ServiceRoleKey);
        _http.DefaultRequestHeaders.Add("apikey", _options.ServiceRoleKey);
    }

    // caminhoNoBucket ex: "login-banners/xxxx.jpg" -> retorna a URL pública
    public async Task<string> UploadAsync(string caminhoNoBucket, Stream conteudo, string contentType)
    {
        using var content = new StreamContent(conteudo);
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"storage/v1/object/{_options.BucketName}/{caminhoNoBucket}")
        {
            Content = content
        };
        request.Headers.Add("x-upsert", "true");

        var response = await _http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(
                $"Falha ao enviar arquivo para o Supabase Storage: {response.StatusCode} - {erro}");
        }

        return $"{_options.Url.TrimEnd('/')}/storage/v1/object/public/{_options.BucketName}/{caminhoNoBucket}";
    }

    public async Task DeleteAsync(string caminhoNoBucket)
    {
        var response = await _http.SendAsync(new HttpRequestMessage(
            HttpMethod.Delete,
            $"storage/v1/object/{_options.BucketName}")
        {
            Content = JsonContent.Create(new { prefixes = new[] { caminhoNoBucket } })
        });

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(
                $"Falha ao remover arquivo do Supabase Storage: {response.StatusCode} - {erro}");
        }
    }
}