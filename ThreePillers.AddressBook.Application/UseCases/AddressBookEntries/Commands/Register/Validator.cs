using ThreePillers.AddressBook.Domain.Extensions;

namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Register;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator(IAddressBookEntryRepository addressBookEntryRepository)
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

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Password is required")
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must have at least 6 chars")
            .MaximumLength(24)
            .WithMessage("Password must have at max 24 chars");

        RuleFor(x => x.ConfirmedPassword)
            .NotNull()
            .WithMessage("Confirmed password is required")
            .NotEmpty()
            .WithMessage("Confirmed password is required")
            .MinimumLength(6)
            .WithMessage("Confirmed password must have at least 6 chars")
            .MaximumLength(24)
            .WithMessage("Confirmed password must have at max 24 chars")
            .Equal(x => x.Password)
            .WithMessage("Confirmed password must be match with password");

        RuleFor(x => x.DateOfBirth)
            .Must(dob => dob <= DateTime.UtcNow.AddYears(-8))
            .WithMessage("Invalid date of birth, sorry you must be 8 years ago");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await addressBookEntryRepository.AnyAsync(
                        x => x.Email.ToLower() == req.Email.ToLower(),
                        ct
                    );
                }
            )
            .WithMessage("Email already registerd");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await addressBookEntryRepository.AnyAsync(
                        x => x.Phone == req.Phone,
                        ct
                    );
                }
            )
            .WithMessage("Phone already registerd");
    }
}
