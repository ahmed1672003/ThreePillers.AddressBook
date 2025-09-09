namespace ThreePillers.AddressBook.Domain.Abstractions;

public abstract class DomainException : Exception
{
    public DomainException(string message)
        : base(message) { }

    public DomainException(string message, Exception? innerException)
        : base(message, innerException) { }

    public DomainException()
        : base() { }
}
