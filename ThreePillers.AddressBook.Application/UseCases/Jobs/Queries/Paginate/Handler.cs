namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Queries.Paginate;

internal sealed class PaginateJobsHandler(IJobRepository jobRepository, IMapper mapper)
    : IRequestHandler<PaginateJobsQuery, PaginationResponse<IEnumerable<JobDto>>>
{
    public async Task<PaginationResponse<IEnumerable<JobDto>>> Handle(
        PaginateJobsQuery request,
        CancellationToken cancellationToken
    )
    {
        var totalCount = await jobRepository.CountAsync(cancellationToken);
        var jobs = await jobRepository.PaginateAsync(
            request.PageSize,
            request.PageIndex,
            request.Search,
            request.SortDirection,
            cancellationToken
        );
        return new()
        {
            Count = jobs.Count(),
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Result = mapper.Map<IEnumerable<JobDto>>(jobs)
        };
    }
}
