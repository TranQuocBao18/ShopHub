namespace ShopHub.Modules.Identity.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string? CreatedByIp { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public Guid? ReplacedById { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt is not null;
    public bool IsActive => !IsRevoked && !IsExpired;

    // EF Core
    private RefreshToken() { }

    public static RefreshToken Create(Guid userId, string token, DateTime expiresAt, string? createdByIp = null)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = createdByIp
        };
    }

    public void Revoke(Guid? replacedById = null)
    {
        RevokedAt = DateTime.UtcNow;
        ReplacedById = replacedById;
    }
}