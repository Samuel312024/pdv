using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class ProdutoFiscalRegraAplicadaConfiguration
    : IEntityTypeConfiguration<ProdutoFiscalRegraAplicada>
{
    public void Configure(EntityTypeBuilder<ProdutoFiscalRegraAplicada> builder)
    {
        builder.ToTable("ProdutoFiscalRegrasAplicadas");

        builder.HasKey(x => x.ProdutoFiscalRegraAplicadaId);

        builder.Property(x => x.Campo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(100);

        builder.Property(x => x.Descricao)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.OrigemRegra)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.RegrasFiscaisAplicadas)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProdutoId,
            x.Ordem
        });

        builder.HasIndex(x => new
        {
            x.ProdutoId,
            x.Campo
        });
    }
}