namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Create;

public sealed record CreateJobCommand(string Title) : IRequest<ResponseOf<JobDto>>;
