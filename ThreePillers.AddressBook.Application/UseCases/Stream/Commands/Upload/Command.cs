using ThreePillers.AddressBook.Application.Abstractions.Storage;

namespace ThreePillers.AddressBook.Application.UseCases.Stream.Commands.Upload;

public sealed record UploadStreamCommand(IFormFile File) : IRequest<ResponseOf<ISupabaseStream>>;
