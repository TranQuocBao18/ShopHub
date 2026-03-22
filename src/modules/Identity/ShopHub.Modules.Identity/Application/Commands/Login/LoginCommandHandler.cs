using Microsoft.AspNetCore.Identity;
using ShopHub.Modules.Identity.Application.DTOs;
using ShopHub.Modules.Identity.Domain.Entities;
using ShopHub.Modules.Identity.Domain.Exceptions;
using ShopHub.Modules.Identity.Domain.Repositories;
using ShopHub.Modules.Identity.Infrastructure.Services;
using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, AuthTokenDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly IIdentityUnitOfWork _uow;

    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtService jwtService,
        IIdentityUnitOfWork uow)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _uow = uow;
    }

    public async Task<Result<AuthTokenDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _uow.Users.GetByEmailAsync(command.Email, cancellationToken)
            ?? throw new InvalidCredentialsException();

        var signInResult = await _signInManager
            .CheckPasswordSignInAsync(user, command.Password, lockoutOnFailure: false);
        if (!signInResult.Succeeded)
            throw new InvalidCredentialsException();

        var roles = await _userManager.GetRolesAsync(user);
        var (accessToken, expiresAt) = _jwtService.GenerateAccessToken(user, roles);
        var rawRefreshToken = _jwtService.GenerateRefreshToken();

        await _uow.BeginTransactionAsync(cancellationToken);
        try
        {
            var activeTokens = await _uow.RefreshTokens.GetActiveByUserIdAsync(user.Id, cancellationToken);
            foreach (var token in activeTokens)
                token.Revoke();

            var newRefreshToken = Domain.Entities.RefreshToken.Create(
                user.Id, rawRefreshToken, DateTime.UtcNow.AddDays(7), command.IpAddress);
            _uow.RefreshTokens.Add(newRefreshToken);

            await _uow.Users.UpdateLastLoginAtAsync(user.Id, DateTime.UtcNow, cancellationToken);

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