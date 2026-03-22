using Microsoft.EntityFrameworkCore;
using ShopHub.Modules.Identity.Domain.Entities;
using ShopHub.Modules.Identity.Domain.Repositories;
using ShopHub.Modules.Identity.Infrastructure.Persistence;

namespace ShopHub.Modules.Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(IdentityDbContext context)
        => _context = context;

    public async Task<ApplicationUser?> GetByEmailAsync(
        string email, CancellationToken cancellationToken = default)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpperInvariant(), cancellationToken);

    public async Task<ApplicationUser?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task UpdateLastLoginAtAsync(
        Guid userId, DateTime lastLoginAt, CancellationToken cancellationToken = default)
        => await _context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(
                u => u.SetProperty(x => x.LastLoginAt, lastLoginAt),
                cancellationToken);
}