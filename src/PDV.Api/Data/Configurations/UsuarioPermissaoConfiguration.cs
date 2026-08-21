using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class UsuarioPermissaoConfiguration : IEntityTypeConfiguration<UsuarioPermissao>
{
    public void Configure(EntityTypeBuilder<UsuarioPermissao> builder)
    {
        builder.ToTable("UsuarioPermissoes");

        builder.HasKey(x => new
        {
            x.UsuarioId,
            x.PermissaoId
        });

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.UsuarioPermissoes)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Permissao)
            .WithMany(x => x.UsuarioPermissoes)
            .HasForeignKey(x => x.PermissaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}