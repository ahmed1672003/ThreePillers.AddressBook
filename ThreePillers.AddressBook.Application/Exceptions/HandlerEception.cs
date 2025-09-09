namespace ThreePillers.AddressBook.Application.Exceptions;

internal sealed class HandlerException : Exception
{
    public HandlerException()
        : base() { }

    public HandlerException(string message)
        : base(message) { }

    public HandlerException(string message, Exception? innerException)
        : base(message, innerException) { }
}
