namespace ThreePillers.AddressBook.infrastructure.Context;

internal sealed class UserContext : IUserContext
{
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly HttpContext _httpContext;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpContext = _httpContextAccessor.HttpContext;
    }

    public (bool Exist, long Value) UserId
    {
        get
        {
            var _userId = _httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return (long.TryParse(_userId, out var userId), userId);
        }
    }

    public (bool Exist, string Value) Email
    {
        get
        {
            var _email = _httpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            return (_email.HasValue(), _email);
        }
    }

    public (bool Exist, string Value) Token
    {
        get
        {
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                var accessToken = _httpContext.Request.Headers["Authorization"].ToString();
                if (accessToken.StartsWith("Bearer "))
                {
                    accessToken = accessToken.Substring("Bearer ".Length);
                }

                if (accessToken is not null)
                    return (true, accessToken);
            }
            return (false, string.Empty);
        }
    }

    public (bool Exist, string Value) IpAddress
    {
        get
        {
            var cip = _httpContext.Connection?.RemoteIpAddress?.ToString();
            return (cip is not null, cip);
        }
    }
}
