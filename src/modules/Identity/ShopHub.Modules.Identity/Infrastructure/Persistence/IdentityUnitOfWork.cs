using Microsoft.EntityFrameworkCore.Storage;
using ShopHub.Modules.Identity.Domain.Repositories;
using ShopHub.Modules.Identity.Infrastructure.Repositories;

namespace ShopHub.Modules.Identity.Infrastructure.Persistence;

public class IdentityUnitOfWork : IIdentityUnitOfWork
{
    private readonly IdentityDbContext _context;
    private IDbContextTransaction? _transaction;

    public IRefreshTokenRepository RefreshTokens { get; }
    public IUserRepository Users { get; }

    public IdentityUnitOfWork(IdentityDbContext context)
    {
        _context = context;
        RefreshTokens = new RefreshTokenRepository(context);
        Users = new UserRepository(context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) throw new InvalidOperationException("Transaction not started.");
        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;
        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}