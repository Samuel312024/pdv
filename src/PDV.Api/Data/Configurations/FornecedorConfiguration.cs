using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.ToTable("Fornecedores");

        builder.HasKey(x => x.FornecedorId);

        builder.Property(x => x.Nome)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Documento)
            .HasMaxLength(20);

        builder.Property(x => x.Telefone)
            .HasMaxLength(30);

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Documento
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Nome
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Ativo
        });
    }
}