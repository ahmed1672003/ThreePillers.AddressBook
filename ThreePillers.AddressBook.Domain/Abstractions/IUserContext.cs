namespace ThreePillers.AddressBook.Domain.Abstractions;

public interface IUserContext
{
    (bool Exist, long Value) UserId { get; }
    (bool Exist, string Value) Token { get; }
    (bool Exist, string Value) Email { get; }
}
