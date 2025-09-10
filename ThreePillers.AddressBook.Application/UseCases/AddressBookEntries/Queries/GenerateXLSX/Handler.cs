using ThreePillers.AddressBook.Application.Abstractions.XLSX;

namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Queries.GenerateXLSX;

internal sealed class GenerateXLSXHandler(
    IXLSXManager xLSXManager,
    IUserContext userContext,
    ISupabaseStorage supabaseStorage,
    IAddressBookEntryRepository addressBookEntryRepository
) : IRequestHandler<GenerateXLSXQuery, ResponseOf<ISupabaseStream>>
{
    public async Task<ResponseOf<ISupabaseStream>> Handle(
        GenerateXLSXQuery request,
        CancellationToken cancellationToken
    )
    {
        var addressBookEntries = await addressBookEntryRepository.LoadAllForGenerationAsync(
            cancellationToken
        );
        var formFile = xLSXManager.GenerateXLSX(addressBookEntries.ToList());
        var result = await supabaseStorage.UploadAsync(
            new UploadSupabaseStream()
            {
                BaseBucket = userContext.UserId.Value.ToString(),
                File = formFile
            },
            cancellationToken
        );
        return new() { Result = result };
    }
}
