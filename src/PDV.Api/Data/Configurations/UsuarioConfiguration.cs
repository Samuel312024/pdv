using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(x => x.UsuarioId);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.CodigoBarrasCracha)
            .HasMaxLength(100);

        builder.Property(x => x.SenhaHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.Email
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.CodigoBarrasCracha
        })
        .IsUnique()
        .HasFilter("\"CodigoBarrasCracha\" IS NOT NULL");

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.Usuarios)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Perfil)
            .WithMany()
            .HasForeignKey(x => x.PerfilId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Cliente)
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}