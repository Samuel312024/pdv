using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalNcmConfiguration : IEntityTypeConfiguration<FiscalNcm>
{
    public void Configure(EntityTypeBuilder<FiscalNcm> builder)
    {
        builder.ToTable("FiscalNcms");

        builder.HasKey(x => x.FiscalNcmId);

        builder.Property(x => x.Codigo)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.DescricaoCompleta)
            .HasMaxLength(2000);

        builder.Property(x => x.AtoLegal)
            .HasMaxLength(500);

        builder.Property(x => x.CestPadraoCodigo)
            .HasMaxLength(20);

        builder.Property(x => x.AliquotaIbpt)
            .HasPrecision(5, 2);

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.Vigente,
            x.Ativo
        });
    }
}