using ThreePillers.AddressBook.Domain.Entities.AddressBookEntries;

namespace ThreePillers.AddressBook.Domain.Entities.Jobs;

public sealed record Job : Entity
{
    public Job()
    {
        AddressBookEntries = new();
    }

    public string Title { get; set; }
    public List<AddressBookEntry> AddressBookEntries { get; set; }
}
