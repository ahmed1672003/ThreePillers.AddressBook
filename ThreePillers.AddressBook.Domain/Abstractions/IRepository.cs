using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ThreePillers.AddressBook.Domain.Enums;

namespace ThreePillers.AddressBook.Domain.Abstractions;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    ValueTask<EntityEntry<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );
    EntityEntry<TEntity> Update(TEntity entity);
    EntityEntry<TEntity> Remove(TEntity entity);
    ValueTask<TEntity> FindByIdForChangeAsync(
        long id,
        CancellationToken cancellationToken = default
    );
    Task<TEntity> FindByIdForReadAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    );
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> PaginateAsync(
        int size,
        int index,
        string search,
        SortDirection sortDirection = SortDirection.Ascending,
        CancellationToken cancellationToken = default
    );

    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    );
}
