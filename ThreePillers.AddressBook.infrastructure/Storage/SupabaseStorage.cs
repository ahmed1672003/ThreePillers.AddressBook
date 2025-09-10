namespace ThreePillers.AddressBook.infrastructure.Storage;

public sealed class SupabaseStorage(Client supabaseClient) : ISupabaseStorage
{
    public async Task<ISupabaseStream> UploadAsync(
        IUploadSupabaseStream contract,
        CancellationToken cancellationToken = default
    )
    {
        await EnsurePickupBucketCreatedAsync(cancellationToken);

        var fileName = $"{Guid.NewGuid()}{contract.Extension}";
        var filePath = $"{contract.BaseBucket}/{contract.Extension}/{fileName}".ToLower();

        await using var memoryStream = new MemoryStream();
        await contract.File.CopyToAsync(memoryStream, cancellationToken);

        var bucket = supabaseClient.From(SupabaseSettings.BaseBucket);

        var supabaseFileUrl = await bucket.Upload(
            memoryStream.ToArray(),
            filePath,
            new Supabase.Storage.FileOptions()
            {
                ContentType = contract.File.ContentType,
                Upsert = false,
            }
        );

        return new SupabaseStream()
        {
            Extensaion = contract.Extension,
            Name = fileName,
            Url = filePath,
            Size = contract.File.Length,
        };
    }

    public async Task DeleteAsync(string url, CancellationToken cancellationToken = default)
    {
        var bucket = supabaseClient.From(SupabaseSettings.BaseBucket);
        await bucket.Remove(url);
    }

    private async Task EnsurePickupBucketCreatedAsync(CancellationToken cancellationToken = default)
    {
        var pickupBucket = await supabaseClient.GetBucket(SupabaseSettings.BaseBucket);

        if (pickupBucket is null)
        {
            await supabaseClient.CreateBucket(
                SupabaseSettings.BaseBucket,
                new BucketUpsertOptions() { Public = true }
            );
        }
    }
}
