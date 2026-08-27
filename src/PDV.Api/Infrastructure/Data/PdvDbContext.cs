using Microsoft.EntityFrameworkCore;
using PDV.Api.Domain;

namespace PDV.Api.Infrastructure.Data;

public class PdvDbContext : DbContext
{
    public PdvDbContext(DbContextOptions<PdvDbContext> options)
        : base(options)
    {
    }

    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<TerminalPdv> TerminaisPdv => Set<TerminalPdv>();

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Perfil> Perfis => Set<Perfil>();
    public DbSet<Permissao> Permissoes => Set<Permissao>();
    public DbSet<PerfilPermissao> PerfilPermissoes => Set<PerfilPermissao>();
    public DbSet<UsuarioPermissao> UsuarioPermissoes => Set<UsuarioPermissao>();

    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<ProdutoFornecedor> ProdutosFornecedores => Set<ProdutoFornecedor>();
    public DbSet<ProdutoCampoPadrao> ProdutosCamposPadrao => Set<ProdutoCampoPadrao>();
    public DbSet<ProdutoCodigo> ProdutosCodigos => Set<ProdutoCodigo>();

    public DbSet<ProdutoLote> ProdutosLotes => Set<ProdutoLote>();
    public DbSet<EstoqueLote> EstoquesLotes => Set<EstoqueLote>();
    public DbSet<MovimentacaoEstoqueLote> MovimentacoesEstoqueLote => Set<MovimentacaoEstoqueLote>();

    public DbSet<DepositoEstoque> DepositosEstoque => Set<DepositoEstoque>();
    public DbSet<EstoqueDeposito> EstoquesDeposito => Set<EstoqueDeposito>();
    public DbSet<TransferenciaEstoque> TransferenciasEstoque => Set<TransferenciaEstoque>();
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();

    public DbSet<FiscalNcm> FiscaisNcm => Set<FiscalNcm>();
    public DbSet<FiscalNcmOficial> FiscaisNcmOficial => Set<FiscalNcmOficial>();
    public DbSet<FiscalCest> FiscaisCest => Set<FiscalCest>();
    public DbSet<FiscalCfop> FiscaisCfop => Set<FiscalCfop>();
    public DbSet<FiscalCsosn> FiscaisCsosn => Set<FiscalCsosn>();
    public DbSet<FiscalCstIcms> FiscaisCstIcms => Set<FiscalCstIcms>();
    public DbSet<FiscalCstPisCofins> FiscaisCstPisCofins => Set<FiscalCstPisCofins>();
    public DbSet<FiscalAliquotaIcms> FiscaisAliquotasIcms => Set<FiscalAliquotaIcms>();
    public DbSet<FiscalBeneficio> FiscaisBeneficios => Set<FiscalBeneficio>();
    public DbSet<FiscalUfParametro> FiscaisUfParametros => Set<FiscalUfParametro>();

    public DbSet<ProdutoFiscalRegraAplicada> ProdutosFiscaisRegrasAplicadas => Set<ProdutoFiscalRegraAplicada>();
    public DbSet<FiscalRegraAplicadaLog> FiscaisRegrasAplicadasLog => Set<FiscalRegraAplicadaLog>();

    public DbSet<Municipio> Municipios => Set<Municipio>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();

    public DbSet<Caixa> Caixas => Set<Caixa>();
    public DbSet<Venda> Vendas => Set<Venda>();
    public DbSet<VendaItem> VendaItens => Set<VendaItem>();
    public DbSet<VendaPagamento> VendaPagamentos => Set<VendaPagamento>();

    public DbSet<PedidoOcorrencia> PedidoOcorrencias => Set<PedidoOcorrencia>();
    public DbSet<Transportadora> Transportadoras => Set<Transportadora>();
    public DbSet<PedidoEntregaLocalizacao> PedidosEntregaLocalizacao => Set<PedidoEntregaLocalizacao>();

    public DbSet<NotaFiscal> NotasFiscais => Set<NotaFiscal>();
    public DbSet<NotaFiscalItem> NotasFiscaisItens => Set<NotaFiscalItem>();
    public DbSet<FiscalSefazUrl> FiscaisSefazUrls => Set<FiscalSefazUrl>();

    public DbSet<LancamentoFinanceiro> LancamentosFinanceiros => Set<LancamentoFinanceiro>();
    public DbSet<CobrancaDigital> CobrancasDigitais => Set<CobrancaDigital>();

    public DbSet<LogSistema> LogsSistema => Set<LogSistema>();

    public object LoginBanners { get; internal set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 4);

        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("timestamp with time zone");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PdvDbContext).Assembly);
    }
}