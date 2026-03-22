using Microsoft.AspNetCore.Identity;
using ShopHub.Modules.Identity.Application.DTOs;
using ShopHub.Modules.Identity.Domain.Entities;
using ShopHub.Modules.Identity.Domain.Enums;
using ShopHub.Modules.Identity.Domain.Exceptions;
using ShopHub.Modules.Identity.Domain.Repositories;
using ShopHub.Modules.Identity.Infrastructure.Services;
using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.Register;

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthTokenDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IIdentityUnitOfWork _uow;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService,
        IIdentityUnitOfWork uow)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _uow = uow;
    }

    public async Task<Result<AuthTokenDto>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var existing = await _uow.Users.GetByEmailAsync(command.Email, cancellationToken);
        if (existing is not null)
            throw new UserAlreadyExistsException(command.Email);

        await _uow.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = command.Email,
                UserName = command.Email,
                FullName = command.FullName,
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, command.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            var roles = await _userManager.GetRolesAsync(user);
            var (accessToken, expiresAt) = _jwtService.GenerateAccessToken(user, roles);
            var rawRefreshToken = _jwtService.GenerateRefreshToken();

            var refreshToken = Domain.Entities.RefreshToken.Create(
                user.Id, rawRefreshToken, DateTime.UtcNow.AddDays(7), command.IpAddress);
            _uow.RefreshTokens.Add(refreshToken);
            await _uow.SaveChangesAsync(cancellationToken);

            await _uow.CommitAsync(cancellationToken);

            return new AuthTokenDto(
                accessToken,
                rawRefreshToken,
                expiresAt,
                new UserDto(user.Id, user.Email!, user.FullName, roles));
        }
        catch
        {
            await _uow.RollbackAsync(cancellationToken);
            throw;
        }
    }
}