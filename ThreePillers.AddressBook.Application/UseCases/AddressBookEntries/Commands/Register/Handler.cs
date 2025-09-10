using ThreePillers.AddressBook.Application.Abstractions.Token;

namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Register;

internal sealed class RegisterHandler(
    IUnitOfWork unitOfWork,
    IAddressBookEntryRepository addressBookEntryRepository,
    IMapper mapper,
    IPasswordHasher<AddressBookEntry> passwordHasher,
    ITokenManager tokenManager
) : IRequestHandler<RegisterCommand, ResponseOf<TokenDto>>
{
    public async Task<ResponseOf<TokenDto>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        var addressBookEntry = mapper.Map<AddressBookEntry>(request);
        addressBookEntry.HashedPassword = passwordHasher.HashPassword(
            addressBookEntry,
            request.Password
        );

        modifiedRows++;
        await addressBookEntryRepository.AddAsync(addressBookEntry, cancellationToken);

        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new()
            {
                Message = "Address book entry registered successfully.",
                Result = tokenManager.GetToken(addressBookEntry)
            };
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new HandlerException();
    }
}
