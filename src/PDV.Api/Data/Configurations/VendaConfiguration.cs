using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.ToTable("Vendas");

        builder.HasKey(x => x.VendaId);

        builder.Property(x => x.NumeroVenda)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Subtotal)
            .HasPrecision(18, 2);

        builder.Property(x => x.DescontoTotal)
            .HasPrecision(18, 2);

        builder.Property(x => x.Total)
            .HasPrecision(18, 2);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.AtendimentoTipo)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.PedidoStatus)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.CodigoAcompanhamento)
            .HasMaxLength(100);

        builder.Property(x => x.ContatoNome)
            .HasMaxLength(200);

        builder.Property(x => x.ContatoTelefone)
            .HasMaxLength(30);

        builder.Property(x => x.ObservacaoPedido)
            .HasMaxLength(2000);

        builder.Property(x => x.EnderecoEntregaSnapshotJson)
            .HasColumnType("text");

        builder.Property(x => x.NomeEntregador)
            .HasMaxLength(200);

        builder.Property(x => x.TelefoneEntregador)
            .HasMaxLength(30);

        builder.Property(x => x.EntregaCodigoAcesso)
            .HasMaxLength(100);

        builder.Property(x => x.EntregaUltimaLatitude)
            .HasPrecision(10, 7);

        builder.Property(x => x.EntregaUltimaLongitude)
            .HasPrecision(10, 7);

        builder.Property(x => x.EntregaPrecisaoMetros)
            .HasPrecision(10, 2);

        builder.Property(x => x.EntregaVelocidadeKmh)
            .HasPrecision(10, 2);

        builder.Property(x => x.EntregaDirecaoGraus)
            .HasPrecision(10, 2);

        builder.Property(x => x.MotivoCancelamento)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Caixa)
            .WithMany(x => x.Vendas)
            .HasForeignKey(x => x.CaixaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Cliente)
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Transportadora)
            .WithMany(x => x.Vendas)
            .HasForeignKey(x => x.TransportadoraId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.EntregadorUsuario)
            .WithMany()
            .HasForeignKey(x => x.EntregadorUsuarioId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.NumeroVenda
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.DataVenda
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Status
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.PedidoStatus
        });

        builder.HasIndex(x => x.ClienteId);

        builder.HasIndex(x => x.CaixaId);
    }
}