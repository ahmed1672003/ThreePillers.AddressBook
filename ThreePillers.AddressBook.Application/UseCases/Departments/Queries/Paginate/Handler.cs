namespace ThreePillers.AddressBook.Application.UseCases.Departments.Queries.Paginate;

internal sealed class PaginateDepartmentsHandler(
    IDepartmentRepository departmentRepository,
    IMapper mapper
) : IRequestHandler<PaginateDepartmentsQuery, PaginationResponse<IEnumerable<DepartmentDto>>>
{
    public async Task<PaginationResponse<IEnumerable<DepartmentDto>>> Handle(
        PaginateDepartmentsQuery request,
        CancellationToken cancellationToken
    )
    {
        var totalCount = await departmentRepository.CountAsync(cancellationToken);
        var departments = await departmentRepository.PaginateAsync(
            request.PageSize,
            request.PageIndex,
            request.Search,
            request.SortDirection,
            cancellationToken
        );
        return new()
        {
            Count = departments.Count(),
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Result = mapper.Map<IEnumerable<DepartmentDto>>(departments)
        };
    }
}
