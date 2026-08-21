using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class TerminalPdvConfiguration : IEntityTypeConfiguration<TerminalPdv>
{
    public void Configure(EntityTypeBuilder<TerminalPdv> builder)
    {
        builder.ToTable("TerminaisPdv");

        builder.HasKey(x => x.TerminalPdvId);

        builder.Property(x => x.CodigoTerminal)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.NomeTerminal)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LojaNome)
            .HasMaxLength(150);

        builder.Property(x => x.EstadoUf)
            .HasMaxLength(2);

        builder.Property(x => x.PerfilInstalacao)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.PerfilImpressora)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.PerfilScanner)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.PerfilTeclado)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Observacao)
            .HasMaxLength(500);

        builder.Property(x => x.ChaveAtivacaoHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.ChaveAtivacaoMascara)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.DispositivoIdentificador)
            .HasMaxLength(200);

        builder.Property(x => x.NomeHost)
            .HasMaxLength(150);

        builder.Property(x => x.VersaoInstalador)
            .HasMaxLength(50);

        builder.Property(x => x.VersaoAplicativo)
            .HasMaxLength(50);

        builder.Property(x => x.UltimoIp)
            .HasMaxLength(45);

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.TerminaisPdv)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.CodigoTerminal
        }).IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.NumeroPdv
        }).IsUnique();
    }
}