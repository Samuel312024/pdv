using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("Empresas");

        builder.HasKey(e => e.EmpresaId);

        builder.Property(e => e.EmpresaId)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.NomeFantasia)
            .HasMaxLength(200);

        builder.Property(e => e.Cnpj)
            .HasMaxLength(14);

        builder.Property(e => e.InscricaoEstadual)
            .HasMaxLength(30);

        builder.Property(e => e.InscricaoMunicipal)
            .HasMaxLength(30);

        builder.Property(e => e.CnaePrincipal)
            .HasMaxLength(10);

        builder.Property(e => e.Telefone)
            .HasMaxLength(20);

        builder.Property(e => e.EmailFiscal)
            .HasMaxLength(200);

        builder.Property(e => e.Cep)
            .HasMaxLength(8);

        builder.Property(e => e.Logradouro)
            .HasMaxLength(200);

        builder.Property(e => e.Numero)
            .HasMaxLength(20);

        builder.Property(e => e.Complemento)
            .HasMaxLength(200);

        builder.Property(e => e.Bairro)
            .HasMaxLength(100);

        builder.Property(e => e.Cidade)
            .HasMaxLength(100);

        builder.Property(e => e.Uf)
            .HasMaxLength(2);

        builder.Property(e => e.CodigoMunicipioIbge)
            .HasMaxLength(7);

        builder.Property(e => e.CertificadoDigitalCaminho)
            .HasMaxLength(500);

        builder.Property(e => e.CertificadoDigitalSenhaProtegida)
            .HasMaxLength(1000);

        builder.Property(e => e.TokenApiFiscalProtegido)
            .HasMaxLength(2000);

        builder.Property(e => e.ApiFiscalClientId)
            .HasMaxLength(500);

        builder.Property(e => e.ApiFiscalClientSecretProtegido)
            .HasMaxLength(2000);

        builder.Property(e => e.UrlApiFiscal)
            .HasMaxLength(500);

        builder.Property(e => e.ApiCobrancaClientId)
            .HasMaxLength(500);

        builder.Property(e => e.ApiCobrancaClientSecretProtegido)
            .HasMaxLength(2000);

        builder.Property(e => e.UrlApiCobranca)
            .HasMaxLength(500);

        builder.Property(e => e.RegimeTributario)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.AmbienteNfe)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(e => e.ProviderFiscal)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.CobrancaDigitalProvider)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.AmbienteCobrancaDigital)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.HasIndex(e => e.Cnpj)
            .IsUnique()
            .HasFilter("\"Cnpj\" IS NOT NULL");

        builder.HasIndex(e => e.Ativo);

        builder.HasMany(e => e.Usuarios)
            .WithOne(u => u.Empresa)
            .HasForeignKey(u => u.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.TerminaisPdv)
            .WithOne(t => t.Empresa)
            .HasForeignKey(t => t.EmpresaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.DepositosEstoque)
            .WithOne(d => d.Empresa)
            .HasForeignKey(d => d.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}