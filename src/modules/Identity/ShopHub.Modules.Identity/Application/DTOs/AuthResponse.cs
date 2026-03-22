namespace ShopHub.Modules.Identity.Application.DTOs;

public record AuthResponse(
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken);
