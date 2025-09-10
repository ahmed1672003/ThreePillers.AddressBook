namespace ThreePillers.AddressBook.Application.UseCases.Stream.Commands.Delete;

public sealed record DeleteStreamCommand(string Url) : IRequest<Response>;
