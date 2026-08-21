using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalCfopConfiguration : IEntityTypeConfiguration<FiscalCfop>
{
    public void Configure(EntityTypeBuilder<FiscalCfop> builder)
    {
        builder.ToTable("FiscalCfops");

        builder.HasKey(x => x.FiscalCfopId);

        builder.Property(x => x.Codigo)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.PerfilFiscalPadrao)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.Entrada,
            x.Saida,
            x.Ativo
        });

        builder.HasIndex(x => new
        {
            x.DentroEstado,
            x.ForaEstado,
            x.Ativo
        });
    }
}
