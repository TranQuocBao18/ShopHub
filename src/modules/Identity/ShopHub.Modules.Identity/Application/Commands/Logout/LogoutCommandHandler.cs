using ShopHub.Modules.Identity.Domain.Exceptions;
using ShopHub.Modules.Identity.Domain.Repositories;
using ShopHub.SharedKernel.Application.Messaging;

namespace ShopHub.Modules.Identity.Application.Commands.Logout;

public sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
{
    private readonly IIdentityUnitOfWork _uow;

    public LogoutCommandHandler(IIdentityUnitOfWork uow)
        => _uow = uow;

    public async Task<Result> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var token = await _uow.RefreshTokens.GetByTokenAsync(command.RefreshToken, cancellationToken)
            ?? throw new InvalidCredentialsException();

        if (!token.IsActive)
            return Result.Success();

        await _uow.BeginTransactionAsync(cancellationToken);
        try
        {
            token.Revoke();
            await _uow.SaveChangesAsync(cancellationToken);
            await _uow.CommitAsync(cancellationToken);
        }
        catch
        {
            await _uow.RollbackAsync(cancellationToken);
            throw;
        }

        return Result.Success();
    }
}