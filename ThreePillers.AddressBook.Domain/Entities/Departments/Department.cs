using ThreePillers.AddressBook.Domain.Entities.AddressBookEntries;

namespace ThreePillers.AddressBook.Domain.Entities.Departments;

public sealed record Department : Entity
{
    public Department()
    {
        AddressBookEntries = new();
    }

    public string Name { get; set; }
    public List<AddressBookEntry> AddressBookEntries { get; set; }
}
