using Microsoft.AspNetCore.Authorization;
using ThreePillers.AddressBook.Application.UseCases.Departments.Queries.Paginate;

namespace ThreePillers.AddressBook.API.Controllers;

[Route("api/v1/department")]
[ApiController]
public class DepartmentController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost()]
    public async Task<ActionResult<ResponseOf<DepartmentDto>>> AddAsync(
        [FromBody] CreateDepartmentCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [Authorize]
    [HttpPut()]
    public async Task<ActionResult<ResponseOf<DepartmentDto>>> UpdateAsync(
        [FromBody] UpdateDepartmentCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [Authorize]
    [HttpDelete("{id:long}")]
    public async Task<ActionResult<Response>> DeleteByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new DeleteDepartmentCommand(id), cancellationToken));

    [Authorize]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<Response>> GetByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new GetDepartmentByIdQuery(id), cancellationToken));

    [AllowAnonymous]
    [HttpGet()]
    public async Task<ActionResult<PaginationResponse<IEnumerable<DepartmentDto>>>> GetByIdAsync(
        [FromQuery] PaginateDepartmentsQuery query,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(query, cancellationToken));
}
