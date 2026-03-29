using System;
using Microsoft.EntityFrameworkCore;
using ShopHub.Domain.Shared.Primitives;
using ShopHub.Infrastructure.Shared.Interfaces;

namespace ShopHub.Infrastructure.Shared.Persistences.Contexts;

public class BaseDbContext<T> : DbContext where T : DbContext
{
    private readonly IDateTimeService _dateTime;
    private readonly IAuthenticatedUserService _authenticatedUser;

    public BaseDbContext(DbContextOptions<T> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _dateTime = dateTime;
        _authenticatedUser = authenticatedUser;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = _dateTime.UtcNow;
                    entry.Entity.CreatedBy = _authenticatedUser.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = _dateTime.UtcNow;
                    entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
