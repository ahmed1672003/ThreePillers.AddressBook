using ThreePillers.AddressBook.Application.Bases.Queries;

namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Queries.Paginate;

public sealed record PaginateJobsQuery
    : PaginateQuery,
        IRequest<PaginationResponse<IEnumerable<JobDto>>>;
