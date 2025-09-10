namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Create;

public sealed record CreateDepartmentCommand(string Name) : IRequest<ResponseOf<DepartmentDto>>;
