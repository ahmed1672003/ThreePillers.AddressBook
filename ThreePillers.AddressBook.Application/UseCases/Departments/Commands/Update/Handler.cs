namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Update;

internal sealed class UpdateDepartmentHandler(
    IUnitOfWork unitOfWork,
    IDepartmentRepository departmentRepository,
    IMapper mapper
) : IRequestHandler<UpdateDepartmentCommand, ResponseOf<DepartmentDto>>
{
    public async Task<ResponseOf<DepartmentDto>> Handle(
        UpdateDepartmentCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(
            IsolationLevel.Snapshot,
            cancellationToken
        );

        var department = await departmentRepository.FindByIdForChangeAsync(
            request.Id,
            cancellationToken
        );

        department.Name = request.Name;

        modifiedRows++;
        departmentRepository.Update(department);

        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new() { Result = mapper.Map<DepartmentDto>(department) };
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new HandlerException();
    }
}
