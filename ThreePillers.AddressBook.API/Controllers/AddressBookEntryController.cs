using Microsoft.AspNetCore.Authorization;
using ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Update;
using ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Queries.Pagiante;

namespace ThreePillers.AddressBook.API.Controllers;

[Route("api/v1/user")]
[ApiController]
public class AddressBookEntryController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<ResponseOf<TokenDto>>> RegisterAsync(
        [FromBody] RegisterCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [AllowAnonymous]
    [HttpPut("auth")]
    public async Task<ActionResult<ResponseOf<TokenDto>>> LoginAsync(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [Authorize]
    [HttpPut()]
    public async Task<ActionResult<ResponseOf<AddressBookEntryDto>>> UpdateAsync(
        [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [Authorize]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ResponseOf<AddressBookEntryDto>>> GetProfileAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new GetProfileQuery(id), cancellationToken));

    [Authorize]
    [HttpGet()]
    public async Task<
        ActionResult<PaginationResponse<IEnumerable<AddressBookEntryDto>>>
    > PaginateAsync(
        [FromQuery] PaginateUsersQuery query,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(query, cancellationToken));

    [Authorize]
    [HttpDelete("{id:long}")]
    public async Task<ActionResult<PaginationResponse<IEnumerable<JobDto>>>> GetByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new DeleteUserCommand(id), cancellationToken));
}
