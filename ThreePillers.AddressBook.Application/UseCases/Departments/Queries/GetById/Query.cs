namespace ThreePillers.AddressBook.Application.UseCases.Departments.Queries.GetById;

public sealed record GetDepartmentByIdQuery(long Id) : IRequest<ResponseOf<DepartmentDto>>;
