using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class EstoqueDepositoConfiguration : IEntityTypeConfiguration<EstoqueDeposito>
{
    public void Configure(EntityTypeBuilder<EstoqueDeposito> builder)
    {
        builder.ToTable("EstoquesDeposito");

        builder.HasKey(x => x.EstoqueDepositoId);

        builder.Property(x => x.QuantidadeDisponivel)
            .HasPrecision(18, 4);

        builder.Property(x => x.QuantidadeReservada)
            .HasPrecision(18, 4);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.DepositoEstoqueId,
            x.ProdutoId
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProdutoId
        });

        builder.HasOne(x => x.DepositoEstoque)
            .WithMany(x => x.Estoques)
            .HasForeignKey(x => x.DepositoEstoqueId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.EstoquesDeposito)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}