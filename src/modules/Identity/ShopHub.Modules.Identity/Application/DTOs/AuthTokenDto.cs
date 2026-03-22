namespace ShopHub.Modules.Identity.Application.DTOs;

public record AuthTokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    UserDto User);