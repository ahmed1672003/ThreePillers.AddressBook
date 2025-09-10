namespace ThreePillers.AddressBook.API.Controllers;

[Route("api/v1/stream")]
[ApiController]
public class StreamController(IMediator mediator, ISupabaseStorage supabaseStorage) : ControllerBase
{
    [HttpPost]
    [EnableRateLimiting("PublicStream")]
    public async Task<ActionResult<ISupabaseStream>> UploadAsync(
        [FromForm] UploadStreamCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [HttpDelete]
    public async Task<ActionResult<Response>> DeleteAsync(
        [FromHeader] string url,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new DeleteStreamCommand(url), cancellationToken));
}
