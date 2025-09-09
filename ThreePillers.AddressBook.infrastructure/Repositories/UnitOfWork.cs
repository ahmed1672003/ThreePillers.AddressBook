using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace ThreePillers.AddressBook.infrastructure.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    readonly AddressBookDbContext _dbContext;

    public UnitOfWork(AddressBookDbContext dbContext) => _dbContext = dbContext;

    public Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default
    ) => _dbContext.Database.BeginTransactionAsync(cancellationToken);

    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default
    ) => _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

    public async Task<bool> SaveChangesAsync(
        int modifiedRows,
        CancellationToken cancellationToken = default
    ) => (await _dbContext.SaveChangesAsync(cancellationToken)) == modifiedRows;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
