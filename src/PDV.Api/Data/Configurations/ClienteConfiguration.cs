using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(x => x.ClienteId);

        builder.Property(x => x.Nome)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Documento)
            .HasMaxLength(20);

        builder.Property(x => x.Segmento)
            .HasMaxLength(100);

        builder.Property(x => x.Telefone)
            .HasMaxLength(30);

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.Cep)
            .HasMaxLength(10);

        builder.Property(x => x.Logradouro)
            .HasMaxLength(300);

        builder.Property(x => x.Numero)
            .HasMaxLength(30);

        builder.Property(x => x.Complemento)
            .HasMaxLength(200);

        builder.Property(x => x.Bairro)
            .HasMaxLength(200);

        builder.Property(x => x.Cidade)
            .HasMaxLength(200);

        builder.Property(x => x.Uf)
            .HasMaxLength(2);

        builder.Property(x => x.CodigoMunicipioIbge)
            .HasMaxLength(10);

        builder.Property(x => x.Endereco)
            .HasMaxLength(1000);

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
            x.EhFornecedor,
            x.Ativo
        });
    }
}