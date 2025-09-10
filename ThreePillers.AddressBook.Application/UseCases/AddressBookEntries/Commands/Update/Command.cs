namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Update;

public sealed record UpdateUserCommand(
    long Id,
    string FullName,
    string PhotoUrl,
    string Email,
    string Phone,
    DateTime DateOfBirth,
    string Address,
    long JobId,
    long DepartmentId
) : IRequest<ResponseOf<AddressBookEntryDto>>;
