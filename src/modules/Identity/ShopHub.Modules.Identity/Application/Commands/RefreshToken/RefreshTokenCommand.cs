using ShopHub.Modules.Identity.Application.DTOs;
using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.RefreshToken;

public record RefreshTokenCommand(
    string Token,
    string? IpAddress) : ICommand<AuthResponse>;
