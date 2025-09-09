namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Create;

internal sealed class CreateDepartmentHandler(
    IUnitOfWork unitOfWork,
    IDepartmentRepository departmentRepository,
    IMapper mapper
) : IRequestHandler<CreateDepartmentCommand, ResponseOf<DepartmentDto>>
{
    public async Task<ResponseOf<DepartmentDto>> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var department = mapper.Map<Department>(request);

        modifiedRows++;
        await departmentRepository.AddAsync(department, cancellationToken);

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
