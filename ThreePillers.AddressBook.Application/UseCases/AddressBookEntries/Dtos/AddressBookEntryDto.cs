namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Dtos;

public sealed record AddressBookEntryDto(
    long Id,
    string FullName,
    string PhotoUrl,
    string Email,
    string Phone,
    DateTime DateOfBirth,
    string Address,
    long JobId,
    long DepartmentId,
    int Age,
    DepartmentDto Department,
    JobDto Job
);
