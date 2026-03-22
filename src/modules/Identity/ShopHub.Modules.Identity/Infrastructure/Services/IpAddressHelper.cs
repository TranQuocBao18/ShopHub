using Microsoft.AspNetCore.Http;

namespace ShopHub.Modules.Identity.Infrastructure.Services;

public static class IpAddressHelper
{
    public static string? GetClientIpAddress(HttpContext context)
    {
        var forwarded = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(forwarded))
            return forwarded.Split(',').First().Trim();

        return context.Connection.RemoteIpAddress?.ToString();
    }
}