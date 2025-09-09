namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Delete;

public sealed record DeleteDepartmentCommand(long Id) : IRequest<Response>;
