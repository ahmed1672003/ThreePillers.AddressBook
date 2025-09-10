using ThreePillers.AddressBook.Domain.Entities.Jobs;

namespace ThreePillers.AddressBook.infrastructure.Repositories;

internal sealed class JobRepository : Repository<Job>, IJobRepository
{
    public JobRepository(AddressBookDbContext dbContext)
        : base(dbContext) { }
}
