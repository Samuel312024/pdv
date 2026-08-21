using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Api.Domain;

namespace PDV.Api.Data.Configurations;

public class PerfilPermissaoConfiguration : IEntityTypeConfiguration<PerfilPermissao>
{
    public void Configure(EntityTypeBuilder<PerfilPermissao> builder)
    {
        builder.ToTable("PerfilPermissoes");

        builder.HasKey(x => new
        {
            x.PerfilId,
            x.PermissaoId
        });

        builder.HasOne(x => x.Perfil)
            .WithMany(x => x.PerfilPermissoes)
            .HasForeignKey(x => x.PerfilId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Permissao)
            .WithMany(x => x.PerfilPermissoes)
            .HasForeignKey(x => x.PermissaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}