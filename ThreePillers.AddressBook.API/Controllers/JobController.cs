using Microsoft.AspNetCore.Authorization;
using ThreePillers.AddressBook.Application.UseCases.Jobs.Queries.Paginate;

namespace ThreePillers.AddressBook.API.Controllers;

[Route("api/v1/job")]
[ApiController]
public class JobController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost()]
    public async Task<ActionResult<ResponseOf<JobDto>>> AddAsync(
        [FromBody] CreateJobCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [Authorize]
    [HttpPut()]
    public async Task<ActionResult<ResponseOf<JobDto>>> UpdateAsync(
        [FromBody] UpdateJobCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [Authorize]
    [HttpDelete("{id:long}")]
    public async Task<ActionResult<Response>> DeleteByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new DeleteJobCommand(id), cancellationToken));

    [Authorize]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<Response>> GetByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new GetJobByIdQuery(id), cancellationToken));

    [AllowAnonymous]
    [HttpGet()]
    public async Task<ActionResult<PaginationResponse<IEnumerable<JobDto>>>> GetByIdAsync(
        [FromQuery] PaginateJobsQuery query,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(query, cancellationToken));
}
