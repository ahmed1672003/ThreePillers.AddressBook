namespace ThreePillers.AddressBook.Application.UseCases.Stream.Commands.Upload;

internal sealed class DeleteStreamHandler(ISupabaseStorage supabaseStorage)
    : IRequestHandler<DeleteStreamCommand, Response>
{
    public async Task<Response> Handle(
        DeleteStreamCommand request,
        CancellationToken cancellationToken
    )
    {
        await supabaseStorage.DeleteAsync(request.Url, cancellationToken);
        return new() { Message = "Photo deleted successfully" };
    }
}
