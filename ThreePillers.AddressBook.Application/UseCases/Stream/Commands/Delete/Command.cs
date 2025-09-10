namespace ThreePillers.AddressBook.Application.UseCases.Stream.Commands.Upload;

public sealed record DeleteStreamCommand(string Url) : IRequest<Response>;
