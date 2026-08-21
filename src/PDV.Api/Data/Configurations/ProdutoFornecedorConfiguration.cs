using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class ProdutoFornecedorConfiguration : IEntityTypeConfiguration<ProdutoFornecedor>
{
    public void Configure(EntityTypeBuilder<ProdutoFornecedor> builder)
    {
        builder.ToTable("ProdutoFornecedores");

        builder.HasKey(x => x.ProdutoFornecedorId);

        builder.Property(x => x.CodigoProdutoFornecedor)
            .HasMaxLength(100);

        builder.Property(x => x.NomeProdutoFornecedor)
            .HasMaxLength(200);

        builder.Property(x => x.PrecoCompra)
            .HasPrecision(18, 4);

        builder.Property(x => x.QuantidadeMinima)
            .HasPrecision(18, 4);

        builder.Property(x => x.UltimoPrecoPago)
            .HasPrecision(18, 4);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProdutoId,
            x.ClienteFornecedorId
        })
        .IsUnique();

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.Fornecedores)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ClienteFornecedor)
            .WithMany()
            .HasForeignKey(x => x.ClienteFornecedorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}