using ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Register;
using ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Update;

namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries;

public sealed class AddressBookEntryProfile : Profile
{
    public AddressBookEntryProfile()
    {
        CreateMap<RegisterCommand, AddressBookEntry>();
        CreateMap<AddressBookEntry, AddressBookEntryDto>();
        CreateMap<UpdateUserCommand, AddressBookEntry>();
    }
}
