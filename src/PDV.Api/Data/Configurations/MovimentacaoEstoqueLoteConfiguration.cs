using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class MovimentacaoEstoqueLoteConfiguration
    : IEntityTypeConfiguration<MovimentacaoEstoqueLote>
{
    public void Configure(EntityTypeBuilder<MovimentacaoEstoqueLote> builder)
    {
        builder.ToTable("MovimentacoesEstoqueLote");

        builder.HasKey(x => x.MovimentacaoEstoqueLoteId);

        builder.Property(x => x.Quantidade)
            .HasPrecision(18, 4);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.MovimentacaoEstoqueId
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProdutoId,
            x.ProdutoLoteId
        });

        builder.HasOne(x => x.MovimentacaoEstoque)
            .WithMany(x => x.Lotes)
            .HasForeignKey(x => x.MovimentacaoEstoqueId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Produto)
            .WithMany()
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ProdutoLote)
            .WithMany()
            .HasForeignKey(x => x.ProdutoLoteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EstoqueLote)
            .WithMany(x => x.Movimentacoes)
            .HasForeignKey(x => x.EstoqueLoteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}