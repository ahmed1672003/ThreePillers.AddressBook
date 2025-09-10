namespace ThreePillers.AddressBook.Application.Abstractions.Storage;

public interface ISupabaseStream
{
    string Name { get; }
    string Url { get; }
    long Size { get; }
    string Extensaion { get; }
}
