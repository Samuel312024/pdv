using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class EstoqueLoteConfiguration : IEntityTypeConfiguration<EstoqueLote>
{
    public void Configure(EntityTypeBuilder<EstoqueLote> builder)
    {
        builder.ToTable("EstoquesLote");

        builder.HasKey(x => x.EstoqueLoteId);

        builder.Property(x => x.QuantidadeEntrada)
            .HasPrecision(18, 4);

        builder.Property(x => x.QuantidadeDisponivel)
            .HasPrecision(18, 4);

        builder.Property(x => x.PrecoCustoUnitario)
            .HasPrecision(18, 4);

        builder.Property(x => x.DocumentoReferencia)
            .HasMaxLength(100);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProdutoId,
            x.ProdutoLoteId
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.DataEntrada
        });

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.EstoquesLote)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ProdutoLote)
            .WithMany(x => x.Estoques)
            .HasForeignKey(x => x.ProdutoLoteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}