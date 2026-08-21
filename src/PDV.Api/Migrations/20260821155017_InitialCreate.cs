using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PDV.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pdv");

            migrationBuilder.CreateTable(
                name: "Categorias",
                schema: "pdv",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "pdv",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Segmento = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    Logradouro = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    Numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Complemento = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Cidade = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    CodigoMunicipioIbge = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    Endereco = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    EhFornecedor = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                schema: "pdv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FriendlyName = table.Column<string>(type: "text", nullable: true),
                    Xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                schema: "pdv",
                columns: table => new
                {
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    NomeFantasia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Cnpj = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    InscricaoEstadualIsento = table.Column<bool>(type: "boolean", nullable: false),
                    InscricaoMunicipal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CnaePrincipal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EmailFiscal = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    Logradouro = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    Numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Complemento = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Cidade = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    CodigoMunicipioIbge = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    CertificadoDigitalCaminho = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CertificadoDigitalSenhaProtegida = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    RegimeTributario = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    AmbienteNfe = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ProviderFiscal = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    UsaIntegracaoDiretaSefaz = table.Column<bool>(type: "boolean", nullable: false),
                    TokenApiFiscalProtegido = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    ApiFiscalClientId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ApiFiscalClientSecretProtegido = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    UrlApiFiscal = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CobrancaDigitalProvider = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    AmbienteCobrancaDigital = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ApiCobrancaClientId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ApiCobrancaClientSecretProtegido = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    UrlApiCobranca = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DiasVencimentoCobranca = table.Column<int>(type: "integer", nullable: false),
                    SerieNfe = table.Column<int>(type: "integer", nullable: false),
                    ProximoNumeroNfe = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.EmpresaId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalAliquotaIcms",
                schema: "pdv",
                columns: table => new
                {
                    FiscalAliquotaIcmsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UfOrigem = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    UfDestino = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    NcmPrefixo = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    OrigemFiscalCodigo = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    CfopCodigo = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    RegimeTributario = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    ConsumidorFinal = table.Column<bool>(type: "boolean", nullable: true),
                    Aliquota = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: false),
                    Prioridade = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalAliquotaIcms", x => x.FiscalAliquotaIcmsId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalBeneficio",
                schema: "pdv",
                columns: table => new
                {
                    FiscalBeneficioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    NcmPrefixo = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalBeneficio", x => x.FiscalBeneficioId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalCest",
                schema: "pdv",
                columns: table => new
                {
                    FiscalCestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    NcmCodigo = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalCest", x => x.FiscalCestId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalCfop",
                schema: "pdv",
                columns: table => new
                {
                    FiscalCfopId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PerfilFiscalPadrao = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Entrada = table.Column<bool>(type: "boolean", nullable: false),
                    Saida = table.Column<bool>(type: "boolean", nullable: false),
                    DentroEstado = table.Column<bool>(type: "boolean", nullable: false),
                    ForaEstado = table.Column<bool>(type: "boolean", nullable: false),
                    ExigeContexto = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalCfop", x => x.FiscalCfopId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalCsosn",
                schema: "pdv",
                columns: table => new
                {
                    FiscalCsosnId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ExigeSt = table.Column<bool>(type: "boolean", nullable: false),
                    DestacaIcmsProprio = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalCsosn", x => x.FiscalCsosnId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalCstIcms",
                schema: "pdv",
                columns: table => new
                {
                    FiscalCstIcmsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ExigeSt = table.Column<bool>(type: "boolean", nullable: false),
                    DestacaIcmsProprio = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalCstIcms", x => x.FiscalCstIcmsId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalCstPisCofins",
                schema: "pdv",
                columns: table => new
                {
                    FiscalCstPisCofinsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    AliquotaZero = table.Column<bool>(type: "boolean", nullable: false),
                    UsaAliquotaPadrao = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalCstPisCofins", x => x.FiscalCstPisCofinsId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalNcm",
                schema: "pdv",
                columns: table => new
                {
                    FiscalNcmId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    DescricaoCompleta = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    AtoLegal = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: true),
                    DataFim = table.Column<DateTime>(type: "date", nullable: true),
                    Vigente = table.Column<bool>(type: "boolean", nullable: false),
                    DataImportacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioImportacao = table.Column<Guid>(type: "uuid", nullable: true),
                    CestPadraoCodigo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    AliquotaIbpt = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    SujeitoSt = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalNcm", x => x.FiscalNcmId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalNcmOficial",
                schema: "pdv",
                columns: table => new
                {
                    FiscalNcmOficialId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CodigoNormalizado = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DescricaoConcatenada = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: true),
                    DataFim = table.Column<DateTime>(type: "date", nullable: true),
                    TipoAtoInicio = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    NumeroAtoInicio = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    AnoAtoInicio = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    EhItemFinal = table.Column<bool>(type: "boolean", nullable: false),
                    Vigente = table.Column<bool>(type: "boolean", nullable: false),
                    DataImportacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalNcmOficial", x => x.FiscalNcmOficialId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalSefazUrls",
                schema: "pdv",
                columns: table => new
                {
                    FiscalSefazUrlId = table.Column<Guid>(type: "uuid", nullable: false),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Ambiente = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Servico = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalSefazUrls", x => x.FiscalSefazUrlId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalUfParametro",
                schema: "pdv",
                columns: table => new
                {
                    FiscalUfParametroId = table.Column<Guid>(type: "uuid", nullable: false),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    AliquotaInternaIcms = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: false),
                    AliquotaInterestadual = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: false),
                    AliquotaFcp = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: false),
                    AliquotaIssPadrao = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    Observacoes = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalUfParametro", x => x.FiscalUfParametroId);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                schema: "pdv",
                columns: table => new
                {
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.FornecedorId);
                });

            migrationBuilder.CreateTable(
                name: "LogsSistema",
                schema: "pdv",
                columns: table => new
                {
                    LogSistemaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    Modulo = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Acao = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Dados = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsSistema", x => x.LogSistemaId);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                schema: "pdv",
                columns: table => new
                {
                    MunicipioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoIbge = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CepInicial = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    CepFinal = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.MunicipioId);
                });

            migrationBuilder.CreateTable(
                name: "Perfis",
                schema: "pdv",
                columns: table => new
                {
                    PerfilId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Nome = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.PerfilId);
                });

            migrationBuilder.CreateTable(
                name: "Permissoes",
                schema: "pdv",
                columns: table => new
                {
                    PermissaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissoes", x => x.PermissaoId);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoCamposPadrao",
                schema: "pdv",
                columns: table => new
                {
                    ProdutoCampoPadraoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Chave = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    ValorPadrao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoCamposPadrao", x => x.ProdutoCampoPadraoId);
                });

            migrationBuilder.CreateTable(
                name: "Transportadoras",
                schema: "pdv",
                columns: table => new
                {
                    TransportadoraId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    NomeFantasia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Responsavel = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    Logradouro = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    Numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Complemento = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Cidade = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    CodigoMunicipioIbge = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    Endereco = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    CorTemaHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    PrazoMedioEntregaMinutos = table.Column<int>(type: "integer", nullable: true),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportadoras", x => x.TransportadoraId);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                schema: "pdv",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClienteFornecedorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CodigoBarras = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Marca = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Ncm = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Cest = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    OrigemFiscal = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    PerfilFiscalPadrao = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CfopVendaPadrao = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CfopVendaInterestadual = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CfopCompraPadrao = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CfopCompraInterestadual = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Csosn = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CstIcms = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CstPis = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CstCofins = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    BeneficioFiscalCodigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CodigoAnp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    UnidadeTributavel = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ExTipi = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    AliquotaIcms = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    AliquotaIpi = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    AliquotaPis = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    AliquotaCofins = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    ImagemUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CatalogoResumo = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    DestaqueCatalogoComprador = table.Column<bool>(type: "boolean", nullable: false),
                    PrecoPromocional = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    PromocaoTitulo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    PromocaoInicioUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PromocaoFimUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CodigoProdutoFornecedor = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    UltimaNotaFiscalCompra = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    DadosExtrasJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecoVenda = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PrecoCusto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EstoqueAtual = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    EstoqueMinimo = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    UnidadeMedida = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    ControlaEstoque = table.Column<bool>(type: "boolean", nullable: false),
                    ControlaLote = table.Column<bool>(type: "boolean", nullable: false),
                    PoliticaBaixaLote = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produtos_Clientes_ClienteFornecedorId",
                        column: x => x.ClienteFornecedorId,
                        principalSchema: "pdv",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                });

            migrationBuilder.CreateTable(
                name: "DepositosEstoque",
                schema: "pdv",
                columns: table => new
                {
                    DepositoEstoqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Padrao = table.Column<bool>(type: "boolean", nullable: false),
                    PermiteVendaDireta = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositosEstoque", x => x.DepositoEstoqueId);
                    table.ForeignKey(
                        name: "FK_DepositosEstoque_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "pdv",
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TerminaisPdv",
                schema: "pdv",
                columns: table => new
                {
                    TerminalPdvId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoTerminal = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    NomeTerminal = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    LojaNome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    EstadoUf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    NumeroPdv = table.Column<int>(type: "integer", nullable: false),
                    PerfilInstalacao = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    PerfilImpressora = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    PerfilScanner = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    PerfilTeclado = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    ImpressaoAutomatica = table.Column<bool>(type: "boolean", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ChaveAtivacaoHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ChaveAtivacaoMascara = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Ativado = table.Column<bool>(type: "boolean", nullable: false),
                    DispositivoIdentificador = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    NomeHost = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    VersaoInstalador = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    VersaoAplicativo = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    UltimoIp = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChaveGeradaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtivadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UltimaSincronizacaoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminaisPdv", x => x.TerminalPdvId);
                    table.ForeignKey(
                        name: "FK_TerminaisPdv_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "pdv",
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "pdv",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PerfilId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: true),
                    Nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CodigoBarrasCracha = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    SenhaHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    UsarPermissoesCustomizadas = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "pdv",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_Usuarios_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "pdv",
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId");
                    table.ForeignKey(
                        name: "FK_Usuarios_Perfis_PerfilId",
                        column: x => x.PerfilId,
                        principalSchema: "pdv",
                        principalTable: "Perfis",
                        principalColumn: "PerfilId");
                });

            migrationBuilder.CreateTable(
                name: "PerfilPermissoes",
                schema: "pdv",
                columns: table => new
                {
                    PerfilId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilPermissoes", x => new { x.PerfilId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_PerfilPermissoes_Perfis_PerfilId",
                        column: x => x.PerfilId,
                        principalSchema: "pdv",
                        principalTable: "Perfis",
                        principalColumn: "PerfilId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilPermissoes_Permissoes_PermissaoId",
                        column: x => x.PermissaoId,
                        principalSchema: "pdv",
                        principalTable: "Permissoes",
                        principalColumn: "PermissaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoCodigos",
                schema: "pdv",
                columns: table => new
                {
                    ProdutoCodigoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Principal = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoCodigos", x => x.ProdutoCodigoId);
                    table.ForeignKey(
                        name: "FK_ProdutoCodigos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoFiscalRegraAplicada",
                schema: "pdv",
                columns: table => new
                {
                    ProdutoFiscalRegraAplicadaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    Campo = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Codigo = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    OrigemRegra = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DataAplicacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoFiscalRegraAplicada", x => x.ProdutoFiscalRegraAplicadaId);
                    table.ForeignKey(
                        name: "FK_ProdutoFiscalRegraAplicada_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoFornecedores",
                schema: "pdv",
                columns: table => new
                {
                    ProdutoFornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteFornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoProdutoFornecedor = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    NomeProdutoFornecedor = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    PrecoCompra = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    QuantidadeMinima = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: true),
                    PrazoEntregaDias = table.Column<int>(type: "integer", nullable: true),
                    UltimaCompraEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UltimoPrecoPago = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    FornecedorPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoFornecedores", x => x.ProdutoFornecedorId);
                    table.ForeignKey(
                        name: "FK_ProdutoFornecedores_Clientes_ClienteFornecedorId",
                        column: x => x.ClienteFornecedorId,
                        principalSchema: "pdv",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_ProdutoFornecedores_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoLotes",
                schema: "pdv",
                columns: table => new
                {
                    ProdutoLoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoLote = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    DataFabricacao = table.Column<DateTime>(type: "date", nullable: true),
                    DataValidade = table.Column<DateTime>(type: "date", nullable: true),
                    Observacao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoLotes", x => x.ProdutoLoteId);
                    table.ForeignKey(
                        name: "FK_ProdutoLotes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstoquesDeposito",
                schema: "pdv",
                columns: table => new
                {
                    EstoqueDepositoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepositoEstoqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeDisponivel = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    QuantidadeReservada = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstoquesDeposito", x => x.EstoqueDepositoId);
                    table.ForeignKey(
                        name: "FK_EstoquesDeposito_DepositosEstoque_DepositoEstoqueId",
                        column: x => x.DepositoEstoqueId,
                        principalSchema: "pdv",
                        principalTable: "DepositosEstoque",
                        principalColumn: "DepositoEstoqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstoquesDeposito_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId");
                });

            migrationBuilder.CreateTable(
                name: "Caixas",
                schema: "pdv",
                columns: table => new
                {
                    CaixaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorInicial = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorDinheiro = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorCartaoCredito = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorCartaoDebito = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorPix = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorVoucher = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotalVendas = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorSangria = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorSuprimento = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DiferencaInformada = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixas", x => x.CaixaId);
                    table.ForeignKey(
                        name: "FK_Caixas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "FiscalRegraAplicadaLog",
                schema: "pdv",
                columns: table => new
                {
                    FiscalRegraAplicadaLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Campo = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    ValorAnterior = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ValorNovo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrigemRegra = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Justificativa = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalRegraAplicadaLog", x => x.FiscalRegraAplicadaLogId);
                    table.ForeignKey(
                        name: "FK_FiscalRegraAplicadaLog_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiscalRegraAplicadaLog_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "MovimentacoesEstoque",
                schema: "pdv",
                columns: table => new
                {
                    MovimentacaoEstoqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    EstoqueAnterior = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    EstoqueAtual = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    EstoqueReservadoAtual = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    Origem = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ReferenciaId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepositoEstoqueId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepositoOrigemId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepositoDestinoId = table.Column<Guid>(type: "uuid", nullable: true),
                    Observacao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DataMovimentacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesEstoque", x => x.MovimentacaoEstoqueId);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_DepositosEstoque_DepositoDestinoId",
                        column: x => x.DepositoDestinoId,
                        principalSchema: "pdv",
                        principalTable: "DepositosEstoque",
                        principalColumn: "DepositoEstoqueId");
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_DepositosEstoque_DepositoEstoqueId",
                        column: x => x.DepositoEstoqueId,
                        principalSchema: "pdv",
                        principalTable: "DepositosEstoque",
                        principalColumn: "DepositoEstoqueId");
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_DepositosEstoque_DepositoOrigemId",
                        column: x => x.DepositoOrigemId,
                        principalSchema: "pdv",
                        principalTable: "DepositosEstoque",
                        principalColumn: "DepositoEstoqueId");
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId");
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "TransferenciasEstoque",
                schema: "pdv",
                columns: table => new
                {
                    TransferenciaEstoqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepositoOrigemId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepositoDestinoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DocumentoReferencia = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Observacao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DataTransferencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferenciasEstoque", x => x.TransferenciaEstoqueId);
                    table.ForeignKey(
                        name: "FK_TransferenciasEstoque_DepositosEstoque_DepositoDestinoId",
                        column: x => x.DepositoDestinoId,
                        principalSchema: "pdv",
                        principalTable: "DepositosEstoque",
                        principalColumn: "DepositoEstoqueId");
                    table.ForeignKey(
                        name: "FK_TransferenciasEstoque_DepositosEstoque_DepositoOrigemId",
                        column: x => x.DepositoOrigemId,
                        principalSchema: "pdv",
                        principalTable: "DepositosEstoque",
                        principalColumn: "DepositoEstoqueId");
                    table.ForeignKey(
                        name: "FK_TransferenciasEstoque_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId");
                    table.ForeignKey(
                        name: "FK_TransferenciasEstoque_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPermissoes",
                schema: "pdv",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPermissoes", x => new { x.UsuarioId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_UsuarioPermissoes_Permissoes_PermissaoId",
                        column: x => x.PermissaoId,
                        principalSchema: "pdv",
                        principalTable: "Permissoes",
                        principalColumn: "PermissaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPermissoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstoquesLote",
                schema: "pdv",
                columns: table => new
                {
                    EstoqueLoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoLoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeEntrada = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    QuantidadeDisponivel = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    PrecoCustoUnitario = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    DataEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DocumentoReferencia = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstoquesLote", x => x.EstoqueLoteId);
                    table.ForeignKey(
                        name: "FK_EstoquesLote_ProdutoLotes_ProdutoLoteId",
                        column: x => x.ProdutoLoteId,
                        principalSchema: "pdv",
                        principalTable: "ProdutoLotes",
                        principalColumn: "ProdutoLoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstoquesLote_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId");
                    table.ForeignKey(
                        name: "FK_EstoquesLote_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                schema: "pdv",
                columns: table => new
                {
                    VendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaixaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: true),
                    NumeroVenda = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    DataVenda = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DescontoTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EhPedido = table.Column<bool>(type: "boolean", nullable: false),
                    AtendimentoTipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PedidoStatus = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CodigoAcompanhamento = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    ContatoNome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ContatoTelefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ObservacaoPedido = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EnderecoEntregaSnapshotJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataUltimaAtualizacaoPedido = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TransportadoraId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntregadorUsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    NomeEntregador = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    TelefoneEntregador = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EntregaCodigoAcesso = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    EntregaCompartilhamentoAtivo = table.Column<bool>(type: "boolean", nullable: false),
                    EntregaUltimaLatitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    EntregaUltimaLongitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    EntregaPrecisaoMetros = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    EntregaVelocidadeKmh = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    EntregaDirecaoGraus = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    EntregaUltimaAtualizacaoGps = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MotivoCancelamento = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.VendaId);
                    table.ForeignKey(
                        name: "FK_Vendas_Caixas_CaixaId",
                        column: x => x.CaixaId,
                        principalSchema: "pdv",
                        principalTable: "Caixas",
                        principalColumn: "CaixaId");
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "pdv",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_Vendas_Transportadoras_TransportadoraId",
                        column: x => x.TransportadoraId,
                        principalSchema: "pdv",
                        principalTable: "Transportadoras",
                        principalColumn: "TransportadoraId");
                    table.ForeignKey(
                        name: "FK_Vendas_Usuarios_EntregadorUsuarioId",
                        column: x => x.EntregadorUsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK_Vendas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "MovimentacoesEstoqueLote",
                schema: "pdv",
                columns: table => new
                {
                    MovimentacaoEstoqueLoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    MovimentacaoEstoqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoLoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstoqueLoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    DataMovimentacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesEstoqueLote", x => x.MovimentacaoEstoqueLoteId);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoqueLote_EstoquesLote_EstoqueLoteId",
                        column: x => x.EstoqueLoteId,
                        principalSchema: "pdv",
                        principalTable: "EstoquesLote",
                        principalColumn: "EstoqueLoteId");
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoqueLote_MovimentacoesEstoque_MovimentacaoE~",
                        column: x => x.MovimentacaoEstoqueId,
                        principalSchema: "pdv",
                        principalTable: "MovimentacoesEstoque",
                        principalColumn: "MovimentacaoEstoqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoqueLote_ProdutoLotes_ProdutoLoteId",
                        column: x => x.ProdutoLoteId,
                        principalSchema: "pdv",
                        principalTable: "ProdutoLotes",
                        principalColumn: "ProdutoLoteId");
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoqueLote_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId");
                });

            migrationBuilder.CreateTable(
                name: "LancamentosFinanceiros",
                schema: "pdv",
                columns: table => new
                {
                    LancamentoFinanceiroId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Origem = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    DocumentoReferencia = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: true),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    CaixaId = table.Column<Guid>(type: "uuid", nullable: true),
                    DataCompetencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataLiquidacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorOriginal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorAcrescimo = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorFinal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorCusto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentosFinanceiros", x => x.LancamentoFinanceiroId);
                    table.ForeignKey(
                        name: "FK_LancamentosFinanceiros_Caixas_CaixaId",
                        column: x => x.CaixaId,
                        principalSchema: "pdv",
                        principalTable: "Caixas",
                        principalColumn: "CaixaId");
                    table.ForeignKey(
                        name: "FK_LancamentosFinanceiros_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "pdv",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_LancamentosFinanceiros_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalSchema: "pdv",
                        principalTable: "Fornecedores",
                        principalColumn: "FornecedorId");
                    table.ForeignKey(
                        name: "FK_LancamentosFinanceiros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK_LancamentosFinanceiros_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalSchema: "pdv",
                        principalTable: "Vendas",
                        principalColumn: "VendaId");
                });

            migrationBuilder.CreateTable(
                name: "NotasFiscais",
                schema: "pdv",
                columns: table => new
                {
                    NotaFiscalId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Serie = table.Column<int>(type: "integer", nullable: false),
                    Ambiente = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Origem = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NumeroVenda = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    DestinatarioNome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DestinatarioDocumento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EmitenteSnapshotJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinatarioSnapshotJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PendenciasJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ValorProdutos = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ProntaParaTransmissao = table.Column<bool>(type: "boolean", nullable: false),
                    ProviderFiscal = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ReferenciaFiscal = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    DocumentoFiscalId = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    ChaveAcesso = table.Column<string>(type: "character varying(44)", maxLength: 44, nullable: true),
                    CodigoNumerico = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    LoteTransmissao = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ReciboSefaz = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ProtocoloAutorizacao = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CodigoStatusSefaz = table.Column<int>(type: "integer", nullable: true),
                    MensagemStatusSefaz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PayloadOriginalJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayloadProviderJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetornoProviderJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanfeUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DanfePdfBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XmlEnvio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XmlRetorno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataTransmissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataAutorizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasFiscais", x => x.NotaFiscalId);
                    table.ForeignKey(
                        name: "FK_NotasFiscais_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "pdv",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_NotasFiscais_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK_NotasFiscais_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalSchema: "pdv",
                        principalTable: "Vendas",
                        principalColumn: "VendaId");
                });

            migrationBuilder.CreateTable(
                name: "PedidoEntregaLocalizacoes",
                schema: "pdv",
                columns: table => new
                {
                    PedidoEntregaLocalizacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    PrecisaoMetros = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    VelocidadeKmh = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    DirecaoGraus = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    DataCaptura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Origem = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Observacao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoEntregaLocalizacoes", x => x.PedidoEntregaLocalizacaoId);
                    table.ForeignKey(
                        name: "FK_PedidoEntregaLocalizacoes_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalSchema: "pdv",
                        principalTable: "Vendas",
                        principalColumn: "VendaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoOcorrencias",
                schema: "pdv",
                columns: table => new
                {
                    PedidoOcorrenciaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Titulo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    VisivelParaCliente = table.Column<bool>(type: "boolean", nullable: false),
                    DataOcorrencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoOcorrencias", x => x.PedidoOcorrenciaId);
                    table.ForeignKey(
                        name: "FK_PedidoOcorrencias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK_PedidoOcorrencias_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalSchema: "pdv",
                        principalTable: "Vendas",
                        principalColumn: "VendaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendaItens",
                schema: "pdv",
                columns: table => new
                {
                    VendaItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Desconto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaItens", x => x.VendaItemId);
                    table.ForeignKey(
                        name: "FK_VendaItens_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId");
                    table.ForeignKey(
                        name: "FK_VendaItens_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalSchema: "pdv",
                        principalTable: "Vendas",
                        principalColumn: "VendaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendaPagamentos",
                schema: "pdv",
                columns: table => new
                {
                    VendaPagamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormaPagamento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CapturaModo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    StatusTransacao = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ProvedorOperacao = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    ReferenciaTransacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CodigoAutorizacao = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    BandeiraCartao = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    UltimosDigitosCartao = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Parcelas = table.Column<int>(type: "integer", nullable: true),
                    ObservacaoOperacao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PayloadOperacaoJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCaptura = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorPago = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Troco = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaPagamentos", x => x.VendaPagamentoId);
                    table.ForeignKey(
                        name: "FK_VendaPagamentos_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalSchema: "pdv",
                        principalTable: "Vendas",
                        principalColumn: "VendaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CobrancasDigitais",
                schema: "pdv",
                columns: table => new
                {
                    CobrancaDigitalId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: true),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: true),
                    LancamentoFinanceiroId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    Provider = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Origem = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    DocumentoReferencia = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    IdentificadorInterno = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    ChargeIdExterno = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    CustomIdExterno = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    StatusExterno = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    PixCopiaECola = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PixQrCodeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinhaDigitavel = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    LinkCobranca = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    LinkBoleto = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    LinkPdf = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ValorOriginal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorPago = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    DataVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataCriacaoProvider = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PayloadCriacaoJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayloadConsultaJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetornoProviderJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobrancasDigitais", x => x.CobrancaDigitalId);
                    table.ForeignKey(
                        name: "FK_CobrancasDigitais_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "pdv",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_CobrancasDigitais_LancamentosFinanceiros_LancamentoFinancei~",
                        column: x => x.LancamentoFinanceiroId,
                        principalSchema: "pdv",
                        principalTable: "LancamentosFinanceiros",
                        principalColumn: "LancamentoFinanceiroId");
                    table.ForeignKey(
                        name: "FK_CobrancasDigitais_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "pdv",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK_CobrancasDigitais_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalSchema: "pdv",
                        principalTable: "Vendas",
                        principalColumn: "VendaId");
                });

            migrationBuilder.CreateTable(
                name: "NotaFiscalItens",
                schema: "pdv",
                columns: table => new
                {
                    NotaFiscalItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotaFiscalId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoNome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UnidadeMedida = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Ncm = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Cest = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    OrigemFiscal = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Cfop = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Csosn = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CstIcms = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CstPis = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CstCofins = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    BeneficioFiscalCodigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CodigoAnp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    UnidadeTributavel = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ExTipi = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    AliquotaIcms = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    AliquotaIpi = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    AliquotaPis = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    AliquotaCofins = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    Quantidade = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Desconto = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscalItens", x => x.NotaFiscalItemId);
                    table.ForeignKey(
                        name: "FK_NotaFiscalItens_NotasFiscais_NotaFiscalId",
                        column: x => x.NotaFiscalId,
                        principalSchema: "pdv",
                        principalTable: "NotasFiscais",
                        principalColumn: "NotaFiscalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotaFiscalItens_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "pdv",
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caixas_EmpresaId_DataAbertura",
                schema: "pdv",
                table: "Caixas",
                columns: new[] { "EmpresaId", "DataAbertura" });

            migrationBuilder.CreateIndex(
                name: "IX_Caixas_EmpresaId_Status",
                schema: "pdv",
                table: "Caixas",
                columns: new[] { "EmpresaId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Caixas_UsuarioId",
                schema: "pdv",
                table: "Caixas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EmpresaId_Nome",
                schema: "pdv",
                table: "Categorias",
                columns: new[] { "EmpresaId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaId_Documento",
                schema: "pdv",
                table: "Clientes",
                columns: new[] { "EmpresaId", "Documento" },
                unique: true,
                filter: "[Documento] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaId_EhFornecedor_Ativo",
                schema: "pdv",
                table: "Clientes",
                columns: new[] { "EmpresaId", "EhFornecedor", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaId_Nome",
                schema: "pdv",
                table: "Clientes",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasDigitais_ClienteId",
                schema: "pdv",
                table: "CobrancasDigitais",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasDigitais_EmpresaId_ChargeIdExterno",
                schema: "pdv",
                table: "CobrancasDigitais",
                columns: new[] { "EmpresaId", "ChargeIdExterno" },
                unique: true,
                filter: "[ChargeIdExterno] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasDigitais_EmpresaId_DataCriacao",
                schema: "pdv",
                table: "CobrancasDigitais",
                columns: new[] { "EmpresaId", "DataCriacao" });

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasDigitais_EmpresaId_Status_Origem",
                schema: "pdv",
                table: "CobrancasDigitais",
                columns: new[] { "EmpresaId", "Status", "Origem" });

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasDigitais_LancamentoFinanceiroId",
                schema: "pdv",
                table: "CobrancasDigitais",
                column: "LancamentoFinanceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasDigitais_UsuarioId",
                schema: "pdv",
                table: "CobrancasDigitais",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasDigitais_VendaId",
                schema: "pdv",
                table: "CobrancasDigitais",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositosEstoque_EmpresaId_Codigo",
                schema: "pdv",
                table: "DepositosEstoque",
                columns: new[] { "EmpresaId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepositosEstoque_EmpresaId_Nome",
                schema: "pdv",
                table: "DepositosEstoque",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_DepositosEstoque_EmpresaId_Padrao",
                schema: "pdv",
                table: "DepositosEstoque",
                columns: new[] { "EmpresaId", "Padrao" });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Ativo",
                schema: "pdv",
                table: "Empresas",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Cnpj",
                schema: "pdv",
                table: "Empresas",
                column: "Cnpj",
                unique: true,
                filter: "\"Cnpj\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesDeposito_DepositoEstoqueId",
                schema: "pdv",
                table: "EstoquesDeposito",
                column: "DepositoEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesDeposito_EmpresaId_DepositoEstoqueId",
                schema: "pdv",
                table: "EstoquesDeposito",
                columns: new[] { "EmpresaId", "DepositoEstoqueId" });

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesDeposito_EmpresaId_DepositoEstoqueId_ProdutoId",
                schema: "pdv",
                table: "EstoquesDeposito",
                columns: new[] { "EmpresaId", "DepositoEstoqueId", "ProdutoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesDeposito_EmpresaId_ProdutoId",
                schema: "pdv",
                table: "EstoquesDeposito",
                columns: new[] { "EmpresaId", "ProdutoId" });

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesDeposito_EmpresaId_ProdutoId_DepositoEstoqueId",
                schema: "pdv",
                table: "EstoquesDeposito",
                columns: new[] { "EmpresaId", "ProdutoId", "DepositoEstoqueId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesDeposito_ProdutoId",
                schema: "pdv",
                table: "EstoquesDeposito",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesLote_EmpresaId_DataEntrada",
                schema: "pdv",
                table: "EstoquesLote",
                columns: new[] { "EmpresaId", "DataEntrada" });

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesLote_EmpresaId_ProdutoId_DataEntrada",
                schema: "pdv",
                table: "EstoquesLote",
                columns: new[] { "EmpresaId", "ProdutoId", "DataEntrada" });

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesLote_EmpresaId_ProdutoId_ProdutoLoteId",
                schema: "pdv",
                table: "EstoquesLote",
                columns: new[] { "EmpresaId", "ProdutoId", "ProdutoLoteId" });

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesLote_ProdutoId_QuantidadeDisponivel",
                schema: "pdv",
                table: "EstoquesLote",
                columns: new[] { "ProdutoId", "QuantidadeDisponivel" });

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesLote_ProdutoLoteId",
                schema: "pdv",
                table: "EstoquesLote",
                column: "ProdutoLoteId");

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesLote_UsuarioId",
                schema: "pdv",
                table: "EstoquesLote",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalAliquotaIcms_CfopCodigo_RegimeTributario_Ativo",
                schema: "pdv",
                table: "FiscalAliquotaIcms",
                columns: new[] { "CfopCodigo", "RegimeTributario", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalAliquotaIcms_Prioridade",
                schema: "pdv",
                table: "FiscalAliquotaIcms",
                column: "Prioridade");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalAliquotaIcms_RegraBusca",
                schema: "pdv",
                table: "FiscalAliquotaIcms",
                columns: new[] { "UfOrigem", "UfDestino", "NcmPrefixo", "CfopCodigo", "RegimeTributario", "Prioridade" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalAliquotaIcms_UfOrigem_UfDestino_NcmPrefixo_Ativo",
                schema: "pdv",
                table: "FiscalAliquotaIcms",
                columns: new[] { "UfOrigem", "UfDestino", "NcmPrefixo", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalBeneficio_Codigo",
                schema: "pdv",
                table: "FiscalBeneficio",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalBeneficio_Uf_NcmPrefixo",
                schema: "pdv",
                table: "FiscalBeneficio",
                columns: new[] { "Uf", "NcmPrefixo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalBeneficio_Uf_NcmPrefixo_Ativo",
                schema: "pdv",
                table: "FiscalBeneficio",
                columns: new[] { "Uf", "NcmPrefixo", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCest_Ativo",
                schema: "pdv",
                table: "FiscalCest",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCest_Codigo",
                schema: "pdv",
                table: "FiscalCest",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCest_NcmCodigo",
                schema: "pdv",
                table: "FiscalCest",
                column: "NcmCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCfop_Codigo",
                schema: "pdv",
                table: "FiscalCfop",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCfop_DentroEstado_ForaEstado_Ativo",
                schema: "pdv",
                table: "FiscalCfop",
                columns: new[] { "DentroEstado", "ForaEstado", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCfop_Entrada_Saida_Ativo",
                schema: "pdv",
                table: "FiscalCfop",
                columns: new[] { "Entrada", "Saida", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCsosn_Codigo",
                schema: "pdv",
                table: "FiscalCsosn",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCsosn_ExigeSt_Ativo",
                schema: "pdv",
                table: "FiscalCsosn",
                columns: new[] { "ExigeSt", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCstIcms_Codigo",
                schema: "pdv",
                table: "FiscalCstIcms",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCstIcms_ExigeSt_Ativo",
                schema: "pdv",
                table: "FiscalCstIcms",
                columns: new[] { "ExigeSt", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCstPisCofins_AliquotaZero_Ativo",
                schema: "pdv",
                table: "FiscalCstPisCofins",
                columns: new[] { "AliquotaZero", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCstPisCofins_Codigo",
                schema: "pdv",
                table: "FiscalCstPisCofins",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCstPisCofins_UsaAliquotaPadrao_Ativo",
                schema: "pdv",
                table: "FiscalCstPisCofins",
                columns: new[] { "UsaAliquotaPadrao", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalNcm_Codigo",
                schema: "pdv",
                table: "FiscalNcm",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalNcm_Vigente_Ativo",
                schema: "pdv",
                table: "FiscalNcm",
                columns: new[] { "Vigente", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalNcmOficial_Codigo",
                schema: "pdv",
                table: "FiscalNcmOficial",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalNcmOficial_CodigoNormalizado",
                schema: "pdv",
                table: "FiscalNcmOficial",
                column: "CodigoNormalizado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalNcmOficial_EhItemFinal_Vigente_Ativo",
                schema: "pdv",
                table: "FiscalNcmOficial",
                columns: new[] { "EhItemFinal", "Vigente", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalNcmOficial_Vigente_Ativo",
                schema: "pdv",
                table: "FiscalNcmOficial",
                columns: new[] { "Vigente", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalRegraAplicadaLog_EmpresaId_ProdutoId_DataAlteracao",
                schema: "pdv",
                table: "FiscalRegraAplicadaLog",
                columns: new[] { "EmpresaId", "ProdutoId", "DataAlteracao" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalRegraAplicadaLog_ProdutoId_DataAlteracao",
                schema: "pdv",
                table: "FiscalRegraAplicadaLog",
                columns: new[] { "ProdutoId", "DataAlteracao" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalRegraAplicadaLog_UsuarioId",
                schema: "pdv",
                table: "FiscalRegraAplicadaLog",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalSefazUrls_Uf_Ambiente_Servico",
                schema: "pdv",
                table: "FiscalSefazUrls",
                columns: new[] { "Uf", "Ambiente", "Servico" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalUfParametro_Ativo",
                schema: "pdv",
                table: "FiscalUfParametro",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalUfParametro_Uf",
                schema: "pdv",
                table: "FiscalUfParametro",
                column: "Uf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_EmpresaId_Ativo",
                schema: "pdv",
                table: "Fornecedores",
                columns: new[] { "EmpresaId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_EmpresaId_Documento",
                schema: "pdv",
                table: "Fornecedores",
                columns: new[] { "EmpresaId", "Documento" });

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_EmpresaId_Nome",
                schema: "pdv",
                table: "Fornecedores",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_CaixaId",
                schema: "pdv",
                table: "LancamentosFinanceiros",
                column: "CaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_ClienteId",
                schema: "pdv",
                table: "LancamentosFinanceiros",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_EmpresaId_DataCompetencia",
                schema: "pdv",
                table: "LancamentosFinanceiros",
                columns: new[] { "EmpresaId", "DataCompetencia" });

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_EmpresaId_Status_Tipo",
                schema: "pdv",
                table: "LancamentosFinanceiros",
                columns: new[] { "EmpresaId", "Status", "Tipo" });

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_FornecedorId",
                schema: "pdv",
                table: "LancamentosFinanceiros",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_UsuarioId",
                schema: "pdv",
                table: "LancamentosFinanceiros",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_VendaId",
                schema: "pdv",
                table: "LancamentosFinanceiros",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_DepositoDestinoId",
                schema: "pdv",
                table: "MovimentacoesEstoque",
                column: "DepositoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_DepositoEstoqueId",
                schema: "pdv",
                table: "MovimentacoesEstoque",
                column: "DepositoEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_DepositoOrigemId",
                schema: "pdv",
                table: "MovimentacoesEstoque",
                column: "DepositoOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_EmpresaId_ProdutoId_DataMovimentacao",
                schema: "pdv",
                table: "MovimentacoesEstoque",
                columns: new[] { "EmpresaId", "ProdutoId", "DataMovimentacao" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_ProdutoId",
                schema: "pdv",
                table: "MovimentacoesEstoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_UsuarioId",
                schema: "pdv",
                table: "MovimentacoesEstoque",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoqueLote_EmpresaId_MovimentacaoEstoqueId",
                schema: "pdv",
                table: "MovimentacoesEstoqueLote",
                columns: new[] { "EmpresaId", "MovimentacaoEstoqueId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoqueLote_EmpresaId_ProdutoId_DataMovimentac~",
                schema: "pdv",
                table: "MovimentacoesEstoqueLote",
                columns: new[] { "EmpresaId", "ProdutoId", "DataMovimentacao" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoqueLote_EmpresaId_ProdutoId_ProdutoLoteId",
                schema: "pdv",
                table: "MovimentacoesEstoqueLote",
                columns: new[] { "EmpresaId", "ProdutoId", "ProdutoLoteId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoqueLote_EstoqueLoteId",
                schema: "pdv",
                table: "MovimentacoesEstoqueLote",
                column: "EstoqueLoteId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoqueLote_MovimentacaoEstoqueId_EstoqueLoteId",
                schema: "pdv",
                table: "MovimentacoesEstoqueLote",
                columns: new[] { "MovimentacaoEstoqueId", "EstoqueLoteId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoqueLote_ProdutoId",
                schema: "pdv",
                table: "MovimentacoesEstoqueLote",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoqueLote_ProdutoLoteId",
                schema: "pdv",
                table: "MovimentacoesEstoqueLote",
                column: "ProdutoLoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_CodigoIbge",
                schema: "pdv",
                table: "Municipios",
                column: "CodigoIbge",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_Uf_Nome",
                schema: "pdv",
                table: "Municipios",
                columns: new[] { "Uf", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscalItens_NotaFiscalId",
                schema: "pdv",
                table: "NotaFiscalItens",
                column: "NotaFiscalId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscalItens_ProdutoId",
                schema: "pdv",
                table: "NotaFiscalItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_ChaveAcesso",
                schema: "pdv",
                table: "NotasFiscais",
                column: "ChaveAcesso",
                unique: true,
                filter: "\"ChaveAcesso\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_ClienteId",
                schema: "pdv",
                table: "NotasFiscais",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaId_ChaveAcesso",
                schema: "pdv",
                table: "NotasFiscais",
                columns: new[] { "EmpresaId", "ChaveAcesso" },
                unique: true,
                filter: "[ChaveAcesso] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaId_Numero_Serie_Ambiente",
                schema: "pdv",
                table: "NotasFiscais",
                columns: new[] { "EmpresaId", "Numero", "Serie", "Ambiente" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaId_Serie_Numero",
                schema: "pdv",
                table: "NotasFiscais",
                columns: new[] { "EmpresaId", "Serie", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaId_Status",
                schema: "pdv",
                table: "NotasFiscais",
                columns: new[] { "EmpresaId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaId_Status_DataEmissao",
                schema: "pdv",
                table: "NotasFiscais",
                columns: new[] { "EmpresaId", "Status", "DataEmissao" });

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaId_VendaId",
                schema: "pdv",
                table: "NotasFiscais",
                columns: new[] { "EmpresaId", "VendaId" },
                unique: true,
                filter: "[VendaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_UsuarioId",
                schema: "pdv",
                table: "NotasFiscais",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_VendaId",
                schema: "pdv",
                table: "NotasFiscais",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoEntregaLocalizacoes_EmpresaId_VendaId_DataCaptura",
                schema: "pdv",
                table: "PedidoEntregaLocalizacoes",
                columns: new[] { "EmpresaId", "VendaId", "DataCaptura" });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoEntregaLocalizacoes_VendaId",
                schema: "pdv",
                table: "PedidoEntregaLocalizacoes",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoOcorrencias_EmpresaId_VendaId_DataOcorrencia",
                schema: "pdv",
                table: "PedidoOcorrencias",
                columns: new[] { "EmpresaId", "VendaId", "DataOcorrencia" });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoOcorrencias_UsuarioId",
                schema: "pdv",
                table: "PedidoOcorrencias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoOcorrencias_VendaId",
                schema: "pdv",
                table: "PedidoOcorrencias",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilPermissoes_PermissaoId",
                schema: "pdv",
                table: "PerfilPermissoes",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_Codigo",
                schema: "pdv",
                table: "Perfis",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissoes_Codigo",
                schema: "pdv",
                table: "Permissoes",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCamposPadrao_EmpresaId_Chave",
                schema: "pdv",
                table: "ProdutoCamposPadrao",
                columns: new[] { "EmpresaId", "Chave" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCamposPadrao_EmpresaId_Ordem",
                schema: "pdv",
                table: "ProdutoCamposPadrao",
                columns: new[] { "EmpresaId", "Ordem" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCodigos_EmpresaId_Codigo",
                schema: "pdv",
                table: "ProdutoCodigos",
                columns: new[] { "EmpresaId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCodigos_ProdutoId_Principal",
                schema: "pdv",
                table: "ProdutoCodigos",
                columns: new[] { "ProdutoId", "Principal" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFiscalRegraAplicada_EmpresaId_ProdutoId_Ordem",
                schema: "pdv",
                table: "ProdutoFiscalRegraAplicada",
                columns: new[] { "EmpresaId", "ProdutoId", "Ordem" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFiscalRegraAplicada_ProdutoId_Campo",
                schema: "pdv",
                table: "ProdutoFiscalRegraAplicada",
                columns: new[] { "ProdutoId", "Campo" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFiscalRegraAplicada_ProdutoId_Ordem",
                schema: "pdv",
                table: "ProdutoFiscalRegraAplicada",
                columns: new[] { "ProdutoId", "Ordem" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedores_ClienteFornecedorId",
                schema: "pdv",
                table: "ProdutoFornecedores",
                column: "ClienteFornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedores_EmpresaId_ClienteFornecedorId_Ativo",
                schema: "pdv",
                table: "ProdutoFornecedores",
                columns: new[] { "EmpresaId", "ClienteFornecedorId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedores_EmpresaId_ProdutoId_ClienteFornecedorId",
                schema: "pdv",
                table: "ProdutoFornecedores",
                columns: new[] { "EmpresaId", "ProdutoId", "ClienteFornecedorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedores_ProdutoId_ClienteFornecedorId",
                schema: "pdv",
                table: "ProdutoFornecedores",
                columns: new[] { "ProdutoId", "ClienteFornecedorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedores_ProdutoId_FornecedorPrincipal",
                schema: "pdv",
                table: "ProdutoFornecedores",
                columns: new[] { "ProdutoId", "FornecedorPrincipal" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoLotes_EmpresaId_DataValidade",
                schema: "pdv",
                table: "ProdutoLotes",
                columns: new[] { "EmpresaId", "DataValidade" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoLotes_EmpresaId_ProdutoId_CodigoLote",
                schema: "pdv",
                table: "ProdutoLotes",
                columns: new[] { "EmpresaId", "ProdutoId", "CodigoLote" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoLotes_EmpresaId_ProdutoId_DataValidade",
                schema: "pdv",
                table: "ProdutoLotes",
                columns: new[] { "EmpresaId", "ProdutoId", "DataValidade" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoLotes_ProdutoId",
                schema: "pdv",
                table: "ProdutoLotes",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ClienteFornecedorId",
                schema: "pdv",
                table: "Produtos",
                column: "ClienteFornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EmpresaId_CodigoBarras",
                schema: "pdv",
                table: "Produtos",
                columns: new[] { "EmpresaId", "CodigoBarras" },
                unique: true,
                filter: "[CodigoBarras] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EmpresaId_Nome",
                schema: "pdv",
                table: "Produtos",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_TerminaisPdv_CodigoTerminal",
                schema: "pdv",
                table: "TerminaisPdv",
                column: "CodigoTerminal",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TerminaisPdv_EmpresaId_Ativo",
                schema: "pdv",
                table: "TerminaisPdv",
                columns: new[] { "EmpresaId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_TerminaisPdv_EmpresaId_CodigoTerminal",
                schema: "pdv",
                table: "TerminaisPdv",
                columns: new[] { "EmpresaId", "CodigoTerminal" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TerminaisPdv_EmpresaId_NumeroPdv",
                schema: "pdv",
                table: "TerminaisPdv",
                columns: new[] { "EmpresaId", "NumeroPdv" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasEstoque_DepositoDestinoId",
                schema: "pdv",
                table: "TransferenciasEstoque",
                column: "DepositoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasEstoque_DepositoOrigemId",
                schema: "pdv",
                table: "TransferenciasEstoque",
                column: "DepositoOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasEstoque_EmpresaId_DataTransferencia",
                schema: "pdv",
                table: "TransferenciasEstoque",
                columns: new[] { "EmpresaId", "DataTransferencia" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasEstoque_EmpresaId_ProdutoId",
                schema: "pdv",
                table: "TransferenciasEstoque",
                columns: new[] { "EmpresaId", "ProdutoId" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasEstoque_EmpresaId_ProdutoId_DataTransferencia",
                schema: "pdv",
                table: "TransferenciasEstoque",
                columns: new[] { "EmpresaId", "ProdutoId", "DataTransferencia" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasEstoque_ProdutoId",
                schema: "pdv",
                table: "TransferenciasEstoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasEstoque_UsuarioId",
                schema: "pdv",
                table: "TransferenciasEstoque",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportadoras_EmpresaId_Ativo",
                schema: "pdv",
                table: "Transportadoras",
                columns: new[] { "EmpresaId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_Transportadoras_EmpresaId_Documento",
                schema: "pdv",
                table: "Transportadoras",
                columns: new[] { "EmpresaId", "Documento" });

            migrationBuilder.CreateIndex(
                name: "IX_Transportadoras_EmpresaId_Nome",
                schema: "pdv",
                table: "Transportadoras",
                columns: new[] { "EmpresaId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPermissoes_PermissaoId",
                schema: "pdv",
                table: "UsuarioPermissoes",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ClienteId",
                schema: "pdv",
                table: "Usuarios",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                schema: "pdv",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpresaId_CodigoBarrasCracha",
                schema: "pdv",
                table: "Usuarios",
                columns: new[] { "EmpresaId", "CodigoBarrasCracha" },
                unique: true,
                filter: "[CodigoBarrasCracha] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpresaId_Email",
                schema: "pdv",
                table: "Usuarios",
                columns: new[] { "EmpresaId", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PerfilId",
                schema: "pdv",
                table: "Usuarios",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_VendaItens_ProdutoId",
                schema: "pdv",
                table: "VendaItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_VendaItens_VendaId",
                schema: "pdv",
                table: "VendaItens",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_VendaPagamentos_VendaId",
                schema: "pdv",
                table: "VendaPagamentos",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_CaixaId",
                schema: "pdv",
                table: "Vendas",
                column: "CaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteId",
                schema: "pdv",
                table: "Vendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_CodigoAcompanhamento",
                schema: "pdv",
                table: "Vendas",
                columns: new[] { "EmpresaId", "CodigoAcompanhamento" },
                unique: true,
                filter: "[CodigoAcompanhamento] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_DataVenda",
                schema: "pdv",
                table: "Vendas",
                columns: new[] { "EmpresaId", "DataVenda" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_EhPedido_DataVenda",
                schema: "pdv",
                table: "Vendas",
                columns: new[] { "EmpresaId", "EhPedido", "DataVenda" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_EntregaCodigoAcesso",
                schema: "pdv",
                table: "Vendas",
                columns: new[] { "EmpresaId", "EntregaCodigoAcesso" },
                unique: true,
                filter: "[EntregaCodigoAcesso] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_EntregadorUsuarioId",
                schema: "pdv",
                table: "Vendas",
                columns: new[] { "EmpresaId", "EntregadorUsuarioId" },
                filter: "[EntregadorUsuarioId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_NumeroVenda",
                schema: "pdv",
                table: "Vendas",
                columns: new[] { "EmpresaId", "NumeroVenda" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_PedidoStatus",
                schema: "pdv",
                table: "Vendas",
                columns: new[] { "EmpresaId", "PedidoStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_Status",
                schema: "pdv",
                table: "Vendas",
                columns: new[] { "EmpresaId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EntregadorUsuarioId",
                schema: "pdv",
                table: "Vendas",
                column: "EntregadorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_TransportadoraId",
                schema: "pdv",
                table: "Vendas",
                column: "TransportadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_UsuarioId",
                schema: "pdv",
                table: "Vendas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "CobrancasDigitais",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "DataProtectionKeys",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "EstoquesDeposito",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalAliquotaIcms",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalBeneficio",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalCest",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalCfop",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalCsosn",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalCstIcms",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalCstPisCofins",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalNcm",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalNcmOficial",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalRegraAplicadaLog",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalSefazUrls",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "FiscalUfParametro",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "LogsSistema",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "MovimentacoesEstoqueLote",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Municipios",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "NotaFiscalItens",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "PedidoEntregaLocalizacoes",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "PedidoOcorrencias",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "PerfilPermissoes",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "ProdutoCamposPadrao",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "ProdutoCodigos",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "ProdutoFiscalRegraAplicada",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "ProdutoFornecedores",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "TerminaisPdv",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "TransferenciasEstoque",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "UsuarioPermissoes",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "VendaItens",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "VendaPagamentos",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "LancamentosFinanceiros",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "EstoquesLote",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "MovimentacoesEstoque",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "NotasFiscais",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Permissoes",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Fornecedores",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "ProdutoLotes",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "DepositosEstoque",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Vendas",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Produtos",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Caixas",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Transportadoras",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Empresas",
                schema: "pdv");

            migrationBuilder.DropTable(
                name: "Perfis",
                schema: "pdv");
        }
    }
}
