namespace ThreePillers.AddressBook.Application.Abstractions.Token;

public interface ITokenManager
{
    TokenDto GetToken(AddressBookEntry user);
    string GetRefreshToken();
}
