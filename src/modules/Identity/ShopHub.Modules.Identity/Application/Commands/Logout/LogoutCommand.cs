using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.Logout;

public sealed record LogoutCommand(string RefreshToken) : ICommand;