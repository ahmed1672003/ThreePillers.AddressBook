namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Update;

public sealed record UpdateJobCommand(long Id, string Title) : IRequest<ResponseOf<JobDto>>;
