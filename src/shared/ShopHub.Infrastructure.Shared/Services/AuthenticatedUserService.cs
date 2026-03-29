using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShopHub.Infrastructure.Shared.Interfaces;

namespace ShopHub.Infrastructure.Shared.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly ILogger<AuthenticatedUserService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor, ILogger<AuthenticatedUserService> logger)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Guid UserGuidId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.Session?.GetString("UserId");
            return Guid.Parse(userId);
        }
    }

    public string UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.Session?.GetString("UserId");
            return userId;
        }
    }

    public string JwtToken
    {
        get
        {
            return _httpContextAccessor.HttpContext?.Request?.Headers["Authorization"];
        }
    }

    public string IPAddressClient
    {
        get
        {
            return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }
    }

    public bool IsAuthenticated
    {
        get
        {
            var isAuthenticated = _httpContextAccessor.HttpContext?.User?.Identities?.FirstOrDefault()?.IsAuthenticated;
            if (!isAuthenticated.HasValue)
            {
                return false;
            }

            return isAuthenticated.Value;
        }
    }

    public string Role
    {
        get
        {
            var role = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            return role;
        }
    }

    public string TenantId
    {
        get
        {
            var tenantId = _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].FirstOrDefault();
            return tenantId;
        }
    }

}