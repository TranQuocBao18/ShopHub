using Microsoft.AspNetCore.Identity;
using ShopHub.Modules.Identity.Domain.Enums;

namespace ShopHub.Modules.Identity.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}