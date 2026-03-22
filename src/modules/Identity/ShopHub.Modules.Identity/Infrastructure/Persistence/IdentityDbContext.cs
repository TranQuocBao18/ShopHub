using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopHub.Modules.Identity.Domain.Entities;
using ShopHub.Modules.Identity.Infrastructure.Services;

namespace ShopHub.Modules.Identity.Infrastructure.Persistence;

public class IdentityDbContext
    : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        Guid,
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>,
      IUnitOfWork
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("identity");
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await base.SaveChangesAsync(cancellationToken);
}