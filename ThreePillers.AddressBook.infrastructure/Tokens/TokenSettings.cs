namespace ThreePillers.AddressBook.infrastructure.Tokens;

public class TokenSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpireDate { get; set; }
}
