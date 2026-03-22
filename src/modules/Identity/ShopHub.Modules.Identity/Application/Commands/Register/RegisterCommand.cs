using ShopHub.Modules.Identity.Application.DTOs;
using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string FullName,
    string? Phone,
    Guid? TenantId) : ICommand<AuthResponse>;
