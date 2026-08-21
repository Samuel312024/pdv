using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDV.Api.Infrastructure.Data.Configurations;

public abstract class EntityConfigurationBase<TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    public abstract void Configure(EntityTypeBuilder<TEntity> builder);

    protected static void ConfigureDecimal(EntityTypeBuilder<TEntity> builder)
    {
        foreach (var property in builder.Metadata.GetProperties()
                     .Where(x => x.ClrType == typeof(decimal)
                              || x.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(18);
            property.SetScale(4);
        }
    }
}