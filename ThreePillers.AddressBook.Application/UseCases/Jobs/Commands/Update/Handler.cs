namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Update;

internal sealed class UpdateJobHandler(
    IUnitOfWork unitOfWork,
    IJobRepository jobRepository,
    IMapper mapper
) : IRequestHandler<UpdateJobCommand, ResponseOf<JobDto>>
{
    public async Task<ResponseOf<JobDto>> Handle(
        UpdateJobCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(
            IsolationLevel.Snapshot,
            cancellationToken
        );

        var department = await jobRepository.FindByIdForChangeAsync(request.Id, cancellationToken);

        department.Title = request.Title;

        modifiedRows++;
        jobRepository.Update(department);

        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new() { Result = mapper.Map<JobDto>(department) };
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new HandlerException();
    }
}
