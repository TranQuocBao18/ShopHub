namespace ShopHub.Modules.Identity.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<Domain.Entities.RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<IList<Domain.Entities.RefreshToken>> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    void Add(Domain.Entities.RefreshToken refreshToken);
}