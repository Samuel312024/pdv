using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class CaixaConfiguration : IEntityTypeConfiguration<Caixa>
{
    public void Configure(EntityTypeBuilder<Caixa> builder)
    {
        builder.ToTable("Caixas");

        builder.HasKey(x => x.CaixaId);

        builder.Property(x => x.ValorInicial)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorDinheiro)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorCartaoCredito)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorCartaoDebito)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorPix)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorVoucher)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorTotalVendas)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorSangria)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorSuprimento)
            .HasPrecision(18, 2);

        builder.Property(x => x.DiferencaInformada)
            .HasPrecision(18, 2);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Status
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.DataAbertura
        });

        builder.HasIndex(x => x.UsuarioId);
    }
}