namespace ThreePillers.AddressBook.infrastructure.Storage;

public sealed class UploadSupabaseStream : IUploadSupabaseStream
{
    public string BaseBucket { get; set; }
    public IFormFile File { get; set; }
}
