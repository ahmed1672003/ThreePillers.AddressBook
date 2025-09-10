namespace ThreePillers.AddressBook.infrastructure.Repositories;

internal sealed class AddressBookEntryRepository
    : Repository<AddressBookEntry>,
        IAddressBookEntryRepository
{
    public AddressBookEntryRepository(AddressBookDbContext dbContext)
        : base(dbContext) { }

    public async Task<AddressBookEntry> GetByEmailForLoginAsync(
        string email,
        CancellationToken cancellationToken = default
    ) => await _entities.FirstAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken);

    public async Task<AddressBookEntry> GetProfileAsync(
        long id,
        CancellationToken cancellationToken = default
    ) =>
        await _entities
            .AsNoTracking()
            .Include(x => x.Job)
            .Include(x => x.Department)
            .FirstAsync(x => x.Id == id, cancellationToken);

    public async Task<IEnumerable<AddressBookEntry>> PaginateAsync(
        int size,
        int index,
        string search,
        long? jobId,
        long? departmentId,
        DateTime? from,
        DateTime? to,
        SortDirection sortDirection = SortDirection.Ascending,
        CancellationToken cancellationToken = default
    )
    {
        var query = _entities
            .AsNoTracking()
            .Include(x => x.Department)
            .Include(x => x.Job)
            .AsQueryable();

        if (jobId.HasValue)
            query = query.Where(x => x.JobId == jobId.Value);

        if (departmentId.HasValue)
            query = query.Where(x => x.DepartmentId == departmentId.Value);

        if (from.HasValue)
            query = query.Where(x => x.DateOfBirth.Year >= from.Value.Year);

        if (to.HasValue)
            query = query.Where(x => x.DateOfBirth.Year <= to.Value.Year);

        query = query.Paginate(index, size);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AddressBookEntry>> LoadAllForGenerationAsync(
        CancellationToken cancellationToken = default
    ) =>
        await _entities
            .AsNoTracking()
            .Include(x => x.Job)
            .Include(x => x.Department)
            .ToListAsync(cancellationToken);
}
