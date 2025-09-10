namespace ThreePillers.AddressBook.Domain.Entities.AddressBookEntries;

public sealed record AddressBookEntry : Entity
{
    public string FullName { get; set; }
    public string PhotoUrl { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age => DateTime.UtcNow.Year - DateOfBirth.Year;
    public string Address { get; set; }
    public string HashedPassword { get; set; }
    public long JobId { get; set; }
    public long DepartmentId { get; set; }
    public Department Department { get; set; }
    public Job Job { get; set; }
}
