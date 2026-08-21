using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
{
    public void Configure(EntityTypeBuilder<Municipio> builder)
    {
        builder.ToTable("Municipios");

        builder.HasKey(x => x.MunicipioId);

        builder.Property(x => x.CodigoIbge)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Nome)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Uf)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.CepInicial)
            .HasMaxLength(10);

        builder.Property(x => x.CepFinal)
            .HasMaxLength(10);

        builder.HasIndex(x => x.CodigoIbge)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.Uf,
            x.Nome
        });
    }
}