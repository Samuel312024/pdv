using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalCestConfiguration : IEntityTypeConfiguration<FiscalCest>
{
    public void Configure(EntityTypeBuilder<FiscalCest> builder)
    {
        builder.ToTable("FiscalCests");

        builder.HasKey(x => x.FiscalCestId);

        builder.Property(x => x.Codigo)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.NcmCodigo)
            .HasMaxLength(20);

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => x.NcmCodigo);

        builder.HasIndex(x => x.Ativo);
    }
}