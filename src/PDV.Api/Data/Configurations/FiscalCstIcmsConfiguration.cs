using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalCstIcmsConfiguration : IEntityTypeConfiguration<FiscalCstIcms>
{
    public void Configure(EntityTypeBuilder<FiscalCstIcms> builder)
    {
        builder.ToTable("FiscalCstIcms");

        builder.HasKey(x => x.FiscalCstIcmsId);

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