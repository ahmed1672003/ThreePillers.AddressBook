namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<ResponseOf<TokenDto>>;
