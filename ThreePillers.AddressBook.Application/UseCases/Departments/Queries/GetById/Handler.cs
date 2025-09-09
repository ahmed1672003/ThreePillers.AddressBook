namespace ThreePillers.AddressBook.Application.UseCases.Departments.Queries.GetById;

internal sealed class GetDepartmentByIdHandler(
    IDepartmentRepository departmentRepository,
    IMapper mapper
) : IRequestHandler<GetDepartmentByIdQuery, ResponseOf<DepartmentDto>>
{
    public async Task<ResponseOf<DepartmentDto>> Handle(
        GetDepartmentByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var department = await departmentRepository.FindByIdForReadAsync(
            request.Id,
            cancellationToken
        );
        return new() { Result = mapper.Map<DepartmentDto>(department) };
    }
}
