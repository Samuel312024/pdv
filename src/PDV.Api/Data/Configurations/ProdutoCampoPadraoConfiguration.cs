using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class ProdutoCampoPadraoConfiguration : IEntityTypeConfiguration<ProdutoCampoPadrao>
{
    public void Configure(EntityTypeBuilder<ProdutoCampoPadrao> builder)
    {
        builder.ToTable("ProdutoCamposPadrao");

        builder.HasKey(x => x.ProdutoCampoPadraoId);

        builder.Property(x => x.Chave)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ValorPadrao)
            .HasMaxLength(1000);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Chave
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Ordem
        });
    }
}