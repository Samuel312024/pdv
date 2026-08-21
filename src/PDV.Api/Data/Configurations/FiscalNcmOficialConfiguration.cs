using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalNcmOficialConfiguration : IEntityTypeConfiguration<FiscalNcmOficial>
{
    public void Configure(EntityTypeBuilder<FiscalNcmOficial> builder)
    {
        builder.ToTable("FiscalNcmsOficiais");

        builder.HasKey(x => x.FiscalNcmOficialId);

        builder.Property(x => x.Codigo)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.CodigoNormalizado)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.DescricaoConcatenada)
            .HasMaxLength(2000);

        builder.Property(x => x.TipoAtoInicio)
            .HasMaxLength(100);

        builder.Property(x => x.NumeroAtoInicio)
            .HasMaxLength(50);

        builder.Property(x => x.AnoAtoInicio)
            .HasMaxLength(10);

        builder.HasIndex(x => x.CodigoNormalizado)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.Vigente,
            x.Ativo
        });

        builder.HasIndex(x => x.Codigo);
    }
}