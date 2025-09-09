namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Delete;

internal sealed class DeleteDepartmentHandler(
    IUnitOfWork unitOfWork,
    IDepartmentRepository departmentRepository
) : IRequestHandler<DeleteDepartmentCommand, Response>
{
    public async Task<Response> Handle(
        DeleteDepartmentCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var department = await departmentRepository.FindByIdForChangeAsync(
            request.Id,
            cancellationToken
        );

        modifiedRows++;
        departmentRepository.Remove(department);

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
