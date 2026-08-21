using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalCsosnConfiguration : IEntityTypeConfiguration<FiscalCsosn>
{
    public void Configure(EntityTypeBuilder<FiscalCsosn> builder)
    {
        builder.ToTable("FiscalCsosns");

        builder.HasKey(x => x.FiscalCsosnId);

        builder.Property(x => x.Codigo)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.ExigeSt,
            x.Ativo
        });
    }
}