namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Queries.GetById;

internal sealed class GetJobByIdHandler(IJobRepository jobRepository, IMapper mapper)
    : IRequestHandler<GetJobByIdQuery, ResponseOf<JobDto>>
{
    public async Task<ResponseOf<JobDto>> Handle(
        GetJobByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var department = await jobRepository.FindByIdForReadAsync(request.Id, cancellationToken);
        return new() { Result = mapper.Map<JobDto>(department) };
    }
}
