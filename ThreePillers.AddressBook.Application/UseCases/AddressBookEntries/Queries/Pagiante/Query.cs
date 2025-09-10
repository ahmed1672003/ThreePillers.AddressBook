using ThreePillers.AddressBook.Application.Bases.Queries;

namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Queries.Pagiante;

public sealed record PaginateUsersQuery(
    long? JobId,
    long? DepartmentId,
    DateTime? From,
    DateTime? To
) : PaginateQuery, IRequest<PaginationResponse<IEnumerable<AddressBookEntryDto>>>;
