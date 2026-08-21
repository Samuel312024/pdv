using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalAliquotaIcmsConfiguration : IEntityTypeConfiguration<FiscalAliquotaIcms>
{
    public void Configure(EntityTypeBuilder<FiscalAliquotaIcms> builder)
    {
        builder.ToTable("FiscalAliquotasIcms");

        builder.HasKey(x => x.FiscalAliquotaIcmsId);

        builder.Property(x => x.UfOrigem)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.UfDestino)
            .HasMaxLength(2);

        builder.Property(x => x.NcmPrefixo)
            .HasMaxLength(10);

        builder.Property(x => x.OrigemFiscalCodigo)
            .HasMaxLength(10);

        builder.Property(x => x.CfopCodigo)
            .HasMaxLength(10);

        builder.Property(x => x.RegimeTributario)
            .HasMaxLength(50);

        builder.Property(x => x.Aliquota)
            .HasPrecision(5, 2);

        builder.Property(x => x.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.UfOrigem,
            x.UfDestino,
            x.NcmPrefixo,
            x.Ativo
        });

        builder.HasIndex(x => new
        {
            x.CfopCodigo,
            x.RegimeTributario,
            x.Ativo
        });

        builder.HasIndex(x => x.Prioridade);
    }
}