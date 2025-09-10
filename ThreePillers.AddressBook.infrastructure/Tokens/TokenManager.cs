namespace ThreePillers.AddressBook.infrastructure.Tokens;

internal sealed class TokenManager : ITokenManager
{
    readonly TokenSettings _tokenSettings;
    readonly IOptions<TokenSettings> _tokenOptions;

    public TokenManager(IOptions<TokenSettings> tokenOptions)
    {
        _tokenOptions = tokenOptions;
        _tokenSettings = tokenOptions.Value;
    }

    public TokenDto GetToken(AddressBookEntry addressBookEntry)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_tokenSettings.Secret)
        );
        var expireDate = DateTime.UtcNow.AddMonths(_tokenSettings.AccessTokenExpireDate);
        var jwtToken = new JwtSecurityToken(
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            claims: GetClaims(addressBookEntry),
            expires: expireDate,
            signingCredentials: new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        var refreshToken = GetRefreshToken();
        return new TokenDto()
        {
            AccessToken = accessToken,
            ExpiredIn = (expireDate - DateTime.UtcNow).TotalHours,
            RefreshToken = refreshToken,
        };
    }

    public string GetRefreshToken() =>
        $"{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}".Replace(
            "-",
            string.Empty
        );

    private List<Claim> GetClaims(AddressBookEntry bookEntry)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, bookEntry.Email),
            new(ClaimTypes.NameIdentifier, bookEntry.Id.ToString()),
            new(ClaimTypes.DateOfBirth, bookEntry.DateOfBirth.ToString()),
            new(ClaimTypes.MobilePhone, bookEntry.Phone),
        };

        return claims;
    }
}
