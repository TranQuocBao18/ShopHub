namespace ShopHub.Infrastructure.Shared.Interfaces;

public interface IAuthenticatedUserService
{
    string UserId { get; }
    string TenantId { get; }
}