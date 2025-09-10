namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Queries.Pagiante;

internal sealed class PaginateUsersHandler(
    IAddressBookEntryRepository addressBookEntryRepository,
    IMapper mapper
) : IRequestHandler<PaginateUsersQuery, PaginationResponse<IEnumerable<AddressBookEntryDto>>>
{
    public async Task<PaginationResponse<IEnumerable<AddressBookEntryDto>>> Handle(
        PaginateUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        var totalCount = await addressBookEntryRepository.CountAsync(cancellationToken);
        var users = await addressBookEntryRepository.PaginateAsync(
            request.PageSize,
            request.PageIndex,
            request.Search,
            request.JobId,
            request.DepartmentId,
            request.From,
            request.To,
            request.SortDirection,
            cancellationToken
        );
        return new()
        {
            Count = users.Count(),
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Result = mapper.Map<IEnumerable<AddressBookEntryDto>>(users)
        };
    }
}
