namespace ThreePillers.AddressBook.infrastructure.Storage;

public sealed class SupabaseStream : ISupabaseStream
{
    public string Name { get; set; }
    public string Url { get; set; }
    public long Size { get; set; }
    public string Extensaion { get; set; }
}
