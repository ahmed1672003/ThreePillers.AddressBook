namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Delete;

public sealed record DeleteJobCommand(long Id) : IRequest<Response>;
