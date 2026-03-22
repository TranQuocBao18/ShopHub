namespace ShopHub.Modules.Identity.Infrastructure.Services;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}