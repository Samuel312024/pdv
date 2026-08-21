using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
{
    public void Configure(EntityTypeBuilder<NotaFiscal> builder)
    {
        builder.ToTable("NotasFiscais");

        builder.HasKey(x => x.NotaFiscalId);

        builder.Property(x => x.Ambiente)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Origem)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.ProviderFiscal)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.NumeroVenda)
            .HasMaxLength(50);

        builder.Property(x => x.DestinatarioNome)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.DestinatarioDocumento)
            .HasMaxLength(30);

        builder.Property(x => x.EmitenteSnapshotJson)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.DestinatarioSnapshotJson)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.PendenciasJson)
            .HasColumnType("text");

        builder.Property(x => x.Observacoes)
            .HasMaxLength(2000);

        builder.Property(x => x.ValorProdutos)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorDesconto)
            .HasPrecision(18, 2);

        builder.Property(x => x.ValorTotal)
            .HasPrecision(18, 2);

        builder.Property(x => x.ReferenciaFiscal)
            .HasMaxLength(200);

        builder.Property(x => x.DocumentoFiscalId)
            .HasMaxLength(200);

        builder.Property(x => x.ChaveAcesso)
            .HasMaxLength(100);

        builder.Property(x => x.CodigoNumerico)
            .HasMaxLength(50);

        builder.Property(x => x.LoteTransmissao)
            .HasMaxLength(100);

        builder.Property(x => x.ReciboSefaz)
            .HasMaxLength(200);

        builder.Property(x => x.ProtocoloAutorizacao)
            .HasMaxLength(200);

        builder.Property(x => x.MensagemStatusSefaz)
            .HasMaxLength(2000);

        builder.Property(x => x.PayloadOriginalJson)
            .HasColumnType("text");

        builder.Property(x => x.PayloadProviderJson)
            .HasColumnType("text");

        builder.Property(x => x.RetornoProviderJson)
            .HasColumnType("text");

        builder.Property(x => x.DanfeUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.DanfePdfBase64)
            .HasColumnType("text");

        builder.Property(x => x.XmlEnvio)
            .HasColumnType("text");

        builder.Property(x => x.XmlRetorno)
            .HasColumnType("text");

        builder.HasOne(x => x.Venda)
            .WithMany()
            .HasForeignKey(x => x.VendaId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Cliente)
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Numero,
            x.Serie,
            x.Ambiente
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Status
        });

        builder.HasIndex(x => x.ChaveAcesso)
            .IsUnique()
            .HasFilter("\"ChaveAcesso\" IS NOT NULL");

        builder.HasIndex(x => x.VendaId);

        builder.HasIndex(x => x.ClienteId);
    }
}