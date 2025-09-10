namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Register;

public sealed record RegisterCommand(
    string FullName,
    string PhotoUrl,
    string Email,
    string Phone,
    DateTime DateOfBirth,
    string Address,
    string Password,
    string ConfirmedPassword,
    long JobId,
    long DepartmentId
) : IRequest<ResponseOf<TokenDto>>;
