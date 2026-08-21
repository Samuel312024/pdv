using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalUfParametroConfiguration : IEntityTypeConfiguration<FiscalUfParametro>
{
    public void Configure(EntityTypeBuilder<FiscalUfParametro> builder)
    {
        builder.ToTable("FiscalUfParametros");

        builder.HasKey(x => x.FiscalUfParametroId);

        builder.Property(x => x.Uf)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.AliquotaInternaIcms)
            .HasPrecision(5, 2);

        builder.Property(x => x.AliquotaInterestadual)
            .HasPrecision(5, 2);

        builder.Property(x => x.AliquotaFcp)
            .HasPrecision(5, 2);

        builder.Property(x => x.AliquotaIssPadrao)
            .HasPrecision(5, 2);

        builder.Property(x => x.Observacoes)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.Uf)
            .IsUnique();

        builder.HasIndex(x => x.Ativo);
    }
}