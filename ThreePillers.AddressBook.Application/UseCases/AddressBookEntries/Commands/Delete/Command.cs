namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Delete;

public sealed record DeleteUserCommand(long Id) : IRequest<Response>;
