namespace ShopHub.Modules.Identity.Domain.Repositories;

public interface IIdentityUnitOfWork
{
    IRefreshTokenRepository RefreshTokens { get; }
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}