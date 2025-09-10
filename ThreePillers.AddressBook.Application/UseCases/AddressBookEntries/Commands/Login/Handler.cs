using ThreePillers.AddressBook.Application.Abstractions.Token;

namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Login;

internal sealed class LoginHandler(
    IAddressBookEntryRepository addressBookEntryRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher<AddressBookEntry> passwordHasher,
    ITokenManager tokenManager
) : IRequestHandler<LoginCommand, ResponseOf<TokenDto>>
{
    public async Task<ResponseOf<TokenDto>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(
            IsolationLevel.Snapshot,
            cancellationToken
        );

        var addressBookEntry = await addressBookEntryRepository.GetByEmailForLoginAsync(
            request.Email,
            cancellationToken
        );

        addressBookEntry.HashedPassword = passwordHasher.HashPassword(
            addressBookEntry,
            request.Password
        );

        modifiedRows++;
        addressBookEntryRepository.Update(addressBookEntry);

        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

        if (success)
        {
            await transaction.CommitAsync(cancellationToken);

            return new()
            {
                Result = tokenManager.GetToken(addressBookEntry),
                Message = "Login successful."
            };
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new HandlerException();
    }
}
