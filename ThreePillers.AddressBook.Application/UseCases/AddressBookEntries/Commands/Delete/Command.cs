namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Delete;

public sealed record DeleteUserCommand(long Id) : IRequest<Response>;
