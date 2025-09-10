namespace ThreePillers.AddressBook.Application.Abstractions.Storage;

public interface ISupabaseStorage
{
    Task<ISupabaseStream> UploadAsync(
        IUploadSupabaseStream contract,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(string url, CancellationToken cancellationToken = default);
}
