using ThreePillers.AddressBook.Domain.Enums;

namespace ThreePillers.AddressBook.Domain.Entities.AddressBookEntries;

public interface IAddressBookEntryRepository : IRepository<AddressBookEntry>
{
    Task<AddressBookEntry> GetByEmailForLoginAsync(
        string email,
        CancellationToken cancellationToken = default
    );
    Task<AddressBookEntry> GetProfileAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AddressBookEntry>> PaginateAsync(
        int size,
        int index,
        string search,
        long? jobId,
        long? departmentId,
        DateTime? from,
        DateTime? to,
        SortDirection sortDirection = SortDirection.Ascending,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<AddressBookEntry>> LoadAllForGenerationAsync(
        CancellationToken cancellationToken = default
    );
}
