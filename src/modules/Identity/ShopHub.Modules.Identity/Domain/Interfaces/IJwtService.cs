using ShopHub.Modules.Identity.Domain.Entities;

namespace ShopHub.Modules.Identity.Domain.Interfaces;

public interface IJwtService
{
    (string AccessToken, DateTime ExpiresAt) GenerateAccessToken(ApplicationUser user, IList<string> roles);
    string GenerateRefreshToken();
}
