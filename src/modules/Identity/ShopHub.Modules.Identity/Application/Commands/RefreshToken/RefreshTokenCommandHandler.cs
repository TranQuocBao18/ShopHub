using Microsoft.AspNetCore.Identity;
using ShopHub.Modules.Identity.Application.DTOs;
using ShopHub.Modules.Identity.Domain.Entities;
using ShopHub.Modules.Identity.Domain.Exceptions;
using ShopHub.Modules.Identity.Domain.Repositories;
using ShopHub.Modules.Identity.Infrastructure.Services;
using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, AuthTokenDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IIdentityUnitOfWork _uow;

    public RefreshTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService,
        IIdentityUnitOfWork uow)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _uow = uow;
    }

    public async Task<Result<AuthTokenDto>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var existingToken = await _uow.RefreshTokens.GetByTokenAsync(command.Token, cancellationToken)
            ?? throw new InvalidCredentialsException();

        if (!existingToken.IsActive)
            throw new InvalidCredentialsException();

        var user = await _uow.Users.GetByIdAsync(existingToken.UserId, cancellationToken)
            ?? throw new InvalidCredentialsException();

        var roles = await _userManager.GetRolesAsync(user);
        var (accessToken, expiresAt) = _jwtService.GenerateAccessToken(user, roles);
        var rawRefreshToken = _jwtService.GenerateRefreshToken();

        await _uow.BeginTransactionAsync(cancellationToken);
        try
        {
            var newRefreshToken = Domain.Entities.RefreshToken.Create(
                user.Id, rawRefreshToken, DateTime.UtcNow.AddDays(7));
            _uow.RefreshTokens.Add(newRefreshToken);
            await _uow.SaveChangesAsync(cancellationToken);

            existingToken.Revoke(
                replacedById: newRefreshToken.Id);
            await _uow.SaveChangesAsync(cancellationToken);

            await _uow.CommitAsync(cancellationToken);
        }
        catch
        {
            await _uow.RollbackAsync(cancellationToken);
            throw;
        }

        return new AuthTokenDto(
            accessToken,
            rawRefreshToken,
            expiresAt,
            new UserDto(user.Id, user.Email!, user.FullName, roles));
    }
}