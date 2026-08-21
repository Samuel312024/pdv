using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(x => x.CategoriaId);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Nome
        })
        .IsUnique();
    }
}