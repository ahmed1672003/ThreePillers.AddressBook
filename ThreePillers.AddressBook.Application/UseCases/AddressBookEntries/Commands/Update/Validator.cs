using ThreePillers.AddressBook.Domain.Extensions;

namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Update;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator(IAddressBookEntryRepository addressBookEntryRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Email is required")
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email address not valid")
            .Must(email => email.IsValidEmail())
            .WithMessage("Email address not valid");

        RuleFor(x => x.Phone)
            .NotNull()
            .WithMessage("Phone is required")
            .NotEmpty()
            .WithMessage("Phone is required")
            .Must(phone => phone.IsValidPhone())
            .WithMessage("Phone is not valid");

        RuleFor(x => x.FullName)
            .NotNull()
            .WithMessage("Name is required")
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(2)
            .WithMessage("Name must have at least 2 chars")
            .MaximumLength(100)
            .WithMessage("Name must have at max 100 chars");

        RuleFor(x => x.Address)
            .NotNull()
            .WithMessage("Address is required")
            .NotEmpty()
            .WithMessage("Address is required")
            .MinimumLength(2)
            .WithMessage("Address must have at least 2 chars")
            .MaximumLength(100)
            .WithMessage("Address must have at max 100 chars");

        RuleFor(x => x.DateOfBirth)
            .Must(dob => dob <= DateTime.UtcNow.AddYears(-8))
            .WithMessage("Invalid date of birth, sorry you must be 8 years ago");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await addressBookEntryRepository.AnyAsync(x => x.Id == req.Id, ct);
                }
            )
            .WithMessage("This user not registerd");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await addressBookEntryRepository.AnyAsync(
                        x => x.Id != req.Id && x.Email.ToLower() == req.Email.ToLower(),
                        ct
                    );
                }
            )
            .WithMessage("This email already registerd");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await addressBookEntryRepository.AnyAsync(
                        x => x.Id != req.Id && x.Phone == req.Phone,
                        ct
                    );
                }
            )
            .WithMessage("This phone already registerd");
    }
}
