using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ThreePillers.AddressBook.infrastructure.Repositories;

internal class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly AddressBookDbContext _dbContext;
    protected readonly DbSet<TEntity> _entities;

    public Repository(AddressBookDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<TEntity>();
    }

    public ValueTask<EntityEntry<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => _entities.AddAsync(entity, cancellationToken);

    public EntityEntry<TEntity> Update(TEntity entity) => _entities.Update(entity);

    public EntityEntry<TEntity> Remove(TEntity entity) => _entities.Remove(entity);

    public ValueTask<TEntity> FindByIdForChangeAsync(
        long id,
        CancellationToken cancellationToken = default
    ) => _entities.FindAsync(id, cancellationToken);

    public Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    ) => _entities.AnyAsync(filter, cancellationToken);

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        _entities.AnyAsync(cancellationToken);

    public Task<TEntity> FindByIdForReadAsync(
        long id,
        CancellationToken cancellationToken = default
    ) => _entities.AsNoTracking().FirstAsync(x => x.Id == id, cancellationToken);
}
