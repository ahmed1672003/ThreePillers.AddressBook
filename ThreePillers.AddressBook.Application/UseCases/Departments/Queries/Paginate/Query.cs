using ThreePillers.AddressBook.Application.Bases.Queries;

namespace ThreePillers.AddressBook.Application.UseCases.Departments.Queries.Paginate;

public sealed record PaginateDepartmentsQuery
    : PaginateQuery,
        IRequest<PaginationResponse<IEnumerable<DepartmentDto>>>;
