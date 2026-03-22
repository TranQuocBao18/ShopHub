using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopHub.Modules.Identity.Domain.Entities;

namespace ShopHub.Modules.Identity.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("users");

        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.UserName).HasColumnName("user_name").HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasColumnName("normalized_user_name").HasMaxLength(256);
        builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasColumnName("normalized_email").HasMaxLength(256);
        builder.Property(u => u.EmailConfirmed).HasColumnName("email_confirmed");
        builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
        builder.Property(u => u.SecurityStamp).HasColumnName("security_stamp");
        builder.Property(u => u.ConcurrencyStamp).HasColumnName("concurrency_stamp").IsConcurrencyToken();
        builder.Property(u => u.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20);
        builder.Property(u => u.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
        builder.Property(u => u.TwoFactorEnabled).HasColumnName("two_factor_enabled");
        builder.Property(u => u.LockoutEnd).HasColumnName("lockout_end");
        builder.Property(u => u.LockoutEnabled).HasColumnName("lockout_enabled");
        builder.Property(u => u.AccessFailedCount).HasColumnName("access_failed_count");

        // Custom columns
        builder.Property(u => u.FullName).HasColumnName("full_name").HasMaxLength(255).IsRequired();
        builder.Property(u => u.Status).HasColumnName("status").IsRequired();
        builder.Property(u => u.LastLoginAt).HasColumnName("last_login_at");
        builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();

        // RefreshTokens
        builder.HasMany(u => u.RefreshTokens)
            .WithOne()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}