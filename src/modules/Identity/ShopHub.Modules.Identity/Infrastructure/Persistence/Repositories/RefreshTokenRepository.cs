using Microsoft.EntityFrameworkCore;
using ShopHub.Modules.Identity.Domain.Repositories;
using ShopHub.Modules.Identity.Infrastructure.Persistence;

namespace ShopHub.Modules.Identity.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IdentityDbContext _context;

    public RefreshTokenRepository(IdentityDbContext context)
        => _context = context;

    public async Task<Domain.Entities.RefreshToken?> GetByTokenAsync(
        string token, CancellationToken cancellationToken = default)
        => await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);

    public async Task<IList<Domain.Entities.RefreshToken>> GetActiveByUserIdAsync(
        Guid userId, CancellationToken cancellationToken = default)
        => await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.RevokedAt == null)
            .ToListAsync(cancellationToken);

    public void Add(Domain.Entities.RefreshToken refreshToken)
        => _context.RefreshTokens.Add(refreshToken);
}