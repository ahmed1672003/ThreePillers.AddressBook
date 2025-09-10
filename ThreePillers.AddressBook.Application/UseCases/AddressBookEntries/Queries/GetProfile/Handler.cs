namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Queries.GetProfile;

internal sealed class GetProfileHandler(
    IAddressBookEntryRepository addressBookEntryRepository,
    IMapper mapper
) : IRequestHandler<GetProfileQuery, ResponseOf<AddressBookEntryDto>>
{
    public async Task<ResponseOf<AddressBookEntryDto>> Handle(
        GetProfileQuery request,
        CancellationToken cancellationToken
    )
    {
        var addressBookEntry = await addressBookEntryRepository.GetProfileAsync(
            request.Id,
            cancellationToken
        );
        var result = mapper.Map<AddressBookEntryDto>(addressBookEntry);
        return new() { Result = result };
    }
}
