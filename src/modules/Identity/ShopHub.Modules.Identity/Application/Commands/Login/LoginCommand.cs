using ShopHub.Modules.Identity.Application.DTOs;
using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password,
    string? IpAddress = null) : ICommand<AuthTokenDto>;