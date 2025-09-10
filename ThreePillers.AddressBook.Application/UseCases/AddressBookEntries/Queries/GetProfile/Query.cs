namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Queries.GetProfile;

public sealed record GetProfileQuery(long Id) : IRequest<ResponseOf<AddressBookEntryDto>>;
