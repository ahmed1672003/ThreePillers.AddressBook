namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Delete;

public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator(IAddressBookEntryRepository addressBookEntryRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .MustAsync(
                async (command, ct) =>
                {
                    return await addressBookEntryRepository.AnyAsync(x => x.Id == command.Id, ct);
                }
            )
            .WithMessage("User not exist");
    }
}
