using ThreePillers.AddressBook.infrastructure.Context;

namespace ThreePillers.AddressBook.infrastructure.Repositories;

internal sealed class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(AddressBookDbContext dbContext)
        : base(dbContext) { }
}
