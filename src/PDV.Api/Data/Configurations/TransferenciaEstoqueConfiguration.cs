using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class TransferenciaEstoqueConfiguration
    : IEntityTypeConfiguration<TransferenciaEstoque>
{
    public void Configure(EntityTypeBuilder<TransferenciaEstoque> builder)
    {
        builder.ToTable("TransferenciasEstoque");

        builder.HasKey(x => x.TransferenciaEstoqueId);

        builder.Property(x => x.TransferenciaEstoqueId)
            .ValueGeneratedNever();

        builder.Property(x => x.EmpresaId)
            .IsRequired();

        builder.Property(x => x.ProdutoId)
            .IsRequired();

        builder.Property(x => x.DepositoOrigemId)
            .IsRequired();

        builder.Property(x => x.DepositoDestinoId)
            .IsRequired();

        builder.Property(x => x.Quantidade)
            .HasPrecision(18, 3)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.DocumentoReferencia)
            .HasMaxLength(100);

        builder.Property(x => x.Observacao)
            .HasMaxLength(500);

        builder.Property(x => x.DataTransferencia)
            .IsRequired();

        builder.Property(x => x.UsuarioId)
            .IsRequired();

        // Produto
        builder.HasOne(x => x.Produto)
            .WithMany()
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Depósito de origem
        builder.HasOne(x => x.DepositoOrigem)
            .WithMany(x => x.TransferenciasOrigem)
            .HasForeignKey(x => x.DepositoOrigemId)
            .OnDelete(DeleteBehavior.Restrict);

        // Depósito de destino
        builder.HasOne(x => x.DepositoDestino)
            .WithMany(x => x.TransferenciasDestino)
            .HasForeignKey(x => x.DepositoDestinoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Usuário responsável
        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.DataTransferencia
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProdutoId
        });
    }
}