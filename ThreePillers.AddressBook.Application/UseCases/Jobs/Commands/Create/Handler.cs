namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Create;

internal sealed class CreateJobHandler(
    IUnitOfWork unitOfWork,
    IJobRepository jobRepository,
    IMapper mapper
) : IRequestHandler<CreateJobCommand, ResponseOf<JobDto>>
{
    public async Task<ResponseOf<JobDto>> Handle(
        CreateJobCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var job = mapper.Map<Job>(request);

        modifiedRows++;
        await jobRepository.AddAsync(job, cancellationToken);

        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new() { Result = mapper.Map<JobDto>(job) };
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new HandlerException();
    }
}
