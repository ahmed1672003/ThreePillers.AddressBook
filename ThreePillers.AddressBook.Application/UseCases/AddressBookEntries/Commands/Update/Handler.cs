namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Update;

internal sealed class UpdateUserHandler(
    IUnitOfWork unitOfWork,
    IAddressBookEntryRepository addressBookEntryRepository,
    IMapper mapper
) : IRequestHandler<UpdateUserCommand, ResponseOf<AddressBookEntryDto>>
{
    public async Task<ResponseOf<AddressBookEntryDto>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(
            IsolationLevel.Snapshot,
            cancellationToken
        );

        var addressBookEntry = await addressBookEntryRepository.FindByIdForChangeAsync(
            request.Id,
            cancellationToken
        );

        mapper.Map(request, addressBookEntry);

        modifiedRows++;
        addressBookEntryRepository.Update(addressBookEntry);

        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new() { Result = mapper.Map<AddressBookEntryDto>(addressBookEntry) };
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new HandlerException();
    }
}
