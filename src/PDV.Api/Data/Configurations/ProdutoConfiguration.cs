using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(x => x.ProdutoId);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Descricao)
            .HasMaxLength(1000);

        builder.Property(x => x.Marca)
            .HasMaxLength(100);

        builder.Property(x => x.CodigoBarras)
            .HasMaxLength(50);

        builder.Property(x => x.Ncm)
            .HasMaxLength(20);

        builder.Property(x => x.Cest)
            .HasMaxLength(20);

        builder.Property(x => x.OrigemFiscal)
            .HasMaxLength(10);

        builder.Property(x => x.CfopVendaPadrao)
            .HasMaxLength(10);

        builder.Property(x => x.CfopVendaInterestadual)
            .HasMaxLength(10);

        builder.Property(x => x.CfopCompraPadrao)
            .HasMaxLength(10);

        builder.Property(x => x.CfopCompraInterestadual)
            .HasMaxLength(10);

        builder.Property(x => x.Csosn)
            .HasMaxLength(10);

        builder.Property(x => x.CstIcms)
            .HasMaxLength(10);

        builder.Property(x => x.CstPis)
            .HasMaxLength(10);

        builder.Property(x => x.CstCofins)
            .HasMaxLength(10);

        builder.Property(x => x.BeneficioFiscalCodigo)
            .HasMaxLength(50);

        builder.Property(x => x.CodigoAnp)
            .HasMaxLength(20);

        builder.Property(x => x.UnidadeTributavel)
            .HasMaxLength(10);

        builder.Property(x => x.ExTipi)
            .HasMaxLength(20);

        builder.Property(x => x.UnidadeMedida)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.PrecoVenda)
            .HasPrecision(18, 4);

        builder.Property(x => x.PrecoCusto)
            .HasPrecision(18, 4);

        builder.Property(x => x.PrecoPromocional)
            .HasPrecision(18, 4);

        builder.Property(x => x.EstoqueAtual)
            .HasPrecision(18, 4);

        builder.Property(x => x.EstoqueMinimo)
            .HasPrecision(18, 4);

        builder.Property(x => x.AliquotaIcms)
            .HasPrecision(10, 4);

        builder.Property(x => x.AliquotaIpi)
            .HasPrecision(10, 4);

        builder.Property(x => x.AliquotaPis)
            .HasPrecision(10, 4);

        builder.Property(x => x.AliquotaCofins)
            .HasPrecision(10, 4);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Nome
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.CodigoBarras
        });

        builder.HasOne(x => x.ClienteFornecedor)
            .WithMany()
            .HasForeignKey(x => x.ClienteFornecedorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}