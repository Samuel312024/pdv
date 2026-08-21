using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class ProdutoCodigoConfiguration : IEntityTypeConfiguration<ProdutoCodigo>
{
    public void Configure(EntityTypeBuilder<ProdutoCodigo> builder)
    {
        builder.ToTable("ProdutoCodigos");

        builder.HasKey(x => x.ProdutoCodigoId);

        builder.Property(x => x.Codigo)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Codigo
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.ProdutoId,
            x.Principal
        });

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.Codigos)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}