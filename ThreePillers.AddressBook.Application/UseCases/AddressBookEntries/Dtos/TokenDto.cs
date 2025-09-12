namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Dtos;

public sealed class TokenDto
{
    public long UserId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public double ExpiredIn { get; set; }
}
