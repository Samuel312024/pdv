using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalCstPisCofinsConfiguration : IEntityTypeConfiguration<FiscalCstPisCofins>
{
    public void Configure(EntityTypeBuilder<FiscalCstPisCofins> builder)
    {
        builder.ToTable("FiscalCstPisCofins");

        builder.HasKey(x => x.FiscalCstPisCofinsId);

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
            x.AliquotaZero,
            x.Ativo
        });

        builder.HasIndex(x => new
        {
            x.UsaAliquotaPadrao,
            x.Ativo
        });
    }
}