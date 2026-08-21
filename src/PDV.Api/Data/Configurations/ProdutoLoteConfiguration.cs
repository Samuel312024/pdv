using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class ProdutoLoteConfiguration : IEntityTypeConfiguration<ProdutoLote>
{
    public void Configure(EntityTypeBuilder<ProdutoLote> builder)
    {
        builder.ToTable("ProdutoLotes");

        builder.HasKey(x => x.ProdutoLoteId);

        builder.Property(x => x.CodigoLote)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Observacao)
            .HasMaxLength(500);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProdutoId,
            x.CodigoLote
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.DataValidade
        });

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.Lotes)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}