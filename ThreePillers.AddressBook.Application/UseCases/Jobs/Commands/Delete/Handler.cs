namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Delete;

internal sealed class DeleteJobHandler(IUnitOfWork unitOfWork, IJobRepository jobRepository)
    : IRequestHandler<DeleteJobCommand, Response>
{
    public async Task<Response> Handle(
        DeleteJobCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var job = await jobRepository.FindByIdForChangeAsync(request.Id, cancellationToken);

        modifiedRows++;
        jobRepository.Remove(job);

        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new();
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new HandlerException();
    }
}
