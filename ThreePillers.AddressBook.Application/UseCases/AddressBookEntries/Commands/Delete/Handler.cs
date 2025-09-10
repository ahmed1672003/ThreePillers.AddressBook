namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Delete;

internal sealed class DeleteUserHandler(
    IUnitOfWork unitOfWork,
    IAddressBookEntryRepository addressBookEntryRepository
) : IRequestHandler<DeleteUserCommand, Response>
{
    public async Task<Response> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var user = await addressBookEntryRepository.FindByIdForChangeAsync(
            request.Id,
            cancellationToken
        );

        modifiedRows++;
        addressBookEntryRepository.Remove(user);

        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new();
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new HandlerException();
    }
}
