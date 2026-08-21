using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalBeneficioConfiguration : IEntityTypeConfiguration<FiscalBeneficio>
{
    public void Configure(EntityTypeBuilder<FiscalBeneficio> builder)
    {
        builder.ToTable("FiscalBeneficios");

        builder.HasKey(x => x.FiscalBeneficioId);

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Uf)
            .HasMaxLength(2);

        builder.Property(x => x.NcmPrefixo)
            .HasMaxLength(10);

        builder.HasIndex(x => x.Codigo);

        builder.HasIndex(x => new
        {
            x.Uf,
            x.NcmPrefixo,
            x.Ativo
        });
    }
}