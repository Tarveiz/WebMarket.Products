using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMarket.Products.Common.Data.Entities.Users;
using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Common.Data.Configurations;

/// <summary>
/// Маппинг пользователя на identity.users.
/// </summary>
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.Id).HasName("pk_users");
        entity.ToTable("users", DataConstants.Schemas.Identity);

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(256);
        entity.Property(e => e.DisplayName).HasColumnName("display_name").HasMaxLength(128);
        entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
    }
}
