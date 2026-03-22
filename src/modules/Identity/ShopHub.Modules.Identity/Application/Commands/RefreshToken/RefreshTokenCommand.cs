using ShopHub.Modules.Identity.Application.DTOs;
using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string Token) : ICommand<AuthTokenDto>;