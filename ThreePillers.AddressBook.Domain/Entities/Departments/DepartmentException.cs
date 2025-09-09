namespace ThreePillers.AddressBook.Domain.Entities.Departments;

public class DepartmentException : DomainException
{
    public DepartmentException(string message, Exception? innerException)
        : base(message, innerException) { }

    public DepartmentException(string message)
        : base(message) { }

    public DepartmentException()
        : base() { }
}
