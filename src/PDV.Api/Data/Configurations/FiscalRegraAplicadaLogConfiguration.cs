using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class FiscalRegraAplicadaLogConfiguration
    : IEntityTypeConfiguration<FiscalRegraAplicadaLog>
{
    public void Configure(EntityTypeBuilder<FiscalRegraAplicadaLog> builder)
    {
        builder.ToTable("FiscalRegrasAplicadasLog");

        builder.HasKey(x => x.FiscalRegraAplicadaLogId);

        builder.Property(x => x.Campo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ValorAnterior)
            .HasMaxLength(1000);

        builder.Property(x => x.ValorNovo)
            .HasMaxLength(1000);

        builder.Property(x => x.OrigemRegra)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(x => x.Justificativa)
            .HasMaxLength(1000);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(50);

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.AuditoriasFiscais)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProdutoId,
            x.DataAlteracao
        });

        builder.HasIndex(x => x.UsuarioId);
    }
}