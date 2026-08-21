using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class DepositoEstoqueConfiguration : IEntityTypeConfiguration<DepositoEstoque>
{
    public void Configure(EntityTypeBuilder<DepositoEstoque> builder)
    {
        builder.ToTable("DepositosEstoque");

        builder.HasKey(x => x.DepositoEstoqueId);

        builder.Property(x => x.Codigo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Descricao)
            .HasMaxLength(500);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Codigo
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Nome
        });

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.DepositosEstoque)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}