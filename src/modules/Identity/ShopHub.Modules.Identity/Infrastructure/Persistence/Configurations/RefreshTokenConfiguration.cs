using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopHub.Modules.Identity.Domain.Entities;

namespace ShopHub.Modules.Identity.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Id).HasColumnName("id");
        builder.Property(rt => rt.UserId).HasColumnName("user_id");
        builder.Property(rt => rt.Token).HasColumnName("token").IsRequired();
        builder.HasIndex(rt => rt.Token).IsUnique();
        builder.Property(rt => rt.ExpiresAt).HasColumnName("expires_at").IsRequired();
        builder.Property(rt => rt.RevokedAt).HasColumnName("revoked_at");
        builder.Property(rt => rt.ReplacedById).HasColumnName("replaced_by_id");
        builder.Property(rt => rt.CreatedByIp).HasColumnName("created_by_ip").HasMaxLength(50);
        builder.Property(rt => rt.CreatedAt).HasColumnName("created_at").IsRequired();
    }
}