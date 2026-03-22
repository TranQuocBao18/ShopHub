namespace ShopHub.Modules.Identity.Application.DTOs;

public record UserDto(
    Guid Id,
    string Email,
    string FullName,
    IList<string> Roles);