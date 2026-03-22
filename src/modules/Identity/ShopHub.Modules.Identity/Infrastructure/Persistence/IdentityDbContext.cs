using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopHub.Modules.Identity.Domain.Entities;
using ShopHub.Modules.Identity.Domain.Interfaces;

namespace ShopHub.Modules.Identity.Infrastructure.Persistence;

public class IdentityDbContext
    : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        Guid,
        IdentityUserClaim<Guid>,
        ApplicationUserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>,
      IUnitOfWork
{
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

        builder.Entity<IdentityUserClaim<Guid>>(b =>
        {
            b.ToTable("user_claims", "identity");
            b.Property(c => c.Id).HasColumnName("id");
            b.Property(c => c.UserId).HasColumnName("user_id");
            b.Property(c => c.ClaimType).HasColumnName("claim_type");
            b.Property(c => c.ClaimValue).HasColumnName("claim_value");
        });

        builder.Entity<IdentityRoleClaim<Guid>>(b =>
        {
            b.ToTable("role_claims", "identity");
            b.Property(c => c.Id).HasColumnName("id");
            b.Property(c => c.RoleId).HasColumnName("role_id");
            b.Property(c => c.ClaimType).HasColumnName("claim_type");
            b.Property(c => c.ClaimValue).HasColumnName("claim_value");
        });

        builder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.ToTable("user_logins", "identity");
            b.Property(l => l.LoginProvider).HasColumnName("login_provider");
            b.Property(l => l.ProviderKey).HasColumnName("provider_key");
            b.Property(l => l.ProviderDisplayName).HasColumnName("provider_display_name");
            b.Property(l => l.UserId).HasColumnName("user_id");
        });

        builder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.ToTable("user_tokens", "identity");
            b.Property(t => t.UserId).HasColumnName("user_id");
            b.Property(t => t.LoginProvider).HasColumnName("login_provider");
            b.Property(t => t.Name).HasColumnName("name");
            b.Property(t => t.Value).HasColumnName("value");
        });
    }
}
