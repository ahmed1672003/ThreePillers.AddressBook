namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Queries.GetById;

public sealed record GetJobByIdQuery(long Id) : IRequest<ResponseOf<JobDto>>;
