namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Queries.GenerateXLSX;

public sealed record GenerateXLSXQuery() : IRequest<ResponseOf<ISupabaseStream>>;
