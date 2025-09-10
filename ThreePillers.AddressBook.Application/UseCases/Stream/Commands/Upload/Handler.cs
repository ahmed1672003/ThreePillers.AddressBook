namespace ThreePillers.AddressBook.Application.UseCases.Stream.Commands.Upload;

internal sealed class UploadStreamHandler(
    ISupabaseStorage supabaseStorage,
    IUserContext userContext
) : IRequestHandler<UploadStreamCommand, ResponseOf<ISupabaseStream>>
{
    public async Task<ResponseOf<ISupabaseStream>> Handle(
        UploadStreamCommand request,
        CancellationToken cancellationToken
    )
    {
        var stream = await supabaseStorage.UploadAsync(
            new UploadSupabaseStream()
            {
                BaseBucket = userContext.UserId.Value.ToString(),
                File = request.File,
            },
            cancellationToken
        );
        return new() { Message = "Photo uploaded successfully", Result = stream };
    }
}
