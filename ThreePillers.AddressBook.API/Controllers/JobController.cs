namespace ThreePillers.AddressBook.API.Controllers;

[Route("api/v1/job")]
[ApiController]
public class JobController(IMediator mediator) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult<ResponseOf<JobDto>>> AddAsync(
        [FromBody] CreateJobCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [HttpPut()]
    public async Task<ActionResult<ResponseOf<JobDto>>> UpdateAsync(
        [FromBody] UpdateJobCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<Response>> DeleteByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new DeleteJobCommand(id), cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Response>> GetByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new GetJobByIdQuery(id), cancellationToken));
}
