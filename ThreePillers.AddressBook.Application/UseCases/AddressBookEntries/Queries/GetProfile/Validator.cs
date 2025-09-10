namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Queries.GetProfile;

public sealed class GetProfileValidator : AbstractValidator<GetProfileQuery>
{
    public GetProfileValidator(IAddressBookEntryRepository addressBookEntryRepository)
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
