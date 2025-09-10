using Microsoft.AspNetCore.Authorization;

namespace ThreePillers.AddressBook.API.Controllers;

[Route("api/v1/stream")]
[ApiController]
public class StreamController(IMediator mediator, ISupabaseStorage supabaseStorage) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    [EnableRateLimiting("PublicStream")]
    public async Task<ActionResult<ISupabaseStream>> UploadAsync(
        [FromForm] UploadStreamCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [AllowAnonymous]
    [HttpDelete]
    public async Task<ActionResult<Response>> DeleteAsync(
        [FromHeader] string url,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new DeleteStreamCommand(url), cancellationToken));
}
