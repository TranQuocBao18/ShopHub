using ShopHub.Modules.Identity.Domain.Entities;

namespace ShopHub.Modules.Identity.Domain.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateLastLoginAtAsync(Guid userId, DateTime lastLoginAt, CancellationToken cancellationToken = default);
}