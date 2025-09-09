namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Update;

public sealed record UpdateDepartmentCommand(long Id, string Name)
    : IRequest<ResponseOf<DepartmentDto>>;
