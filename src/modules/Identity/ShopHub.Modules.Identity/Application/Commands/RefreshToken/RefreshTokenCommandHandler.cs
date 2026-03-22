using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ShopHub.Modules.Identity.Application.DTOs;
using ShopHub.Modules.Identity.Domain.Entities;
using ShopHub.Modules.Identity.Domain.Interfaces;
using ShopHub.Modules.Identity.Infrastructure.Services;
using ShopHub.SharedKernel.Application.Messaging;
using RefreshTokenEntity = ShopHub.Modules.Identity.Domain.Entities.RefreshToken;

namespace ShopHub.Modules.Identity.Application.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;

    public RefreshTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(request.Token, cancellationToken);
        if (existingToken is null || !existingToken.IsActive)
            return Result.Failure<AuthResponse>("Invalid or expired refresh token.");

        var user = await _userManager.FindByIdAsync(existingToken.UserId.ToString());
        if (user is null || !user.IsActive || user.IsDeleted)
            return Result.Failure<AuthResponse>("User not found.");

        var roles = await _userManager.GetRolesAsync(user);
        var (accessToken, expiresAt) = _jwtService.GenerateAccessToken(user, roles);
        var newRefreshTokenValue = _jwtService.GenerateRefreshToken();

        var newRefreshToken = new RefreshTokenEntity
        {
            UserId = user.Id,
            Token = newRefreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            CreatedByIp = request.IpAddress
        };

        existingToken.RevokedAt = DateTime.UtcNow;
        existingToken.ReplacedBy = newRefreshToken.Id;

        await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new AuthResponse(accessToken, expiresAt, newRefreshTokenValue));
    }
}
