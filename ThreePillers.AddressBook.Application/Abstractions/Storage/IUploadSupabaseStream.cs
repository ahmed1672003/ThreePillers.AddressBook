namespace ThreePillers.AddressBook.Application.Abstractions.Storage;

public interface IUploadSupabaseStream
{
    string BaseBucket { get; }
    IFormFile File { get; }
    string Extension
    {
        get
        {
            if (File.Length >= 1)
            {
                return Path.GetExtension(File.FileName);
            }
            throw new Exception("Can't read file extension because File is empty");
        }
    }
}
