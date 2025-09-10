namespace ThreePillers.AddressBook.Application.Abstractions.XLSX;

public interface IXLSXManager
{
    IFormFile GenerateXLSX(
        List<AddressBookEntry> addressBookEntries,
        CancellationToken cancellationToken = default
    );
}
