using ThreePillers.AddressBook.Domain.Extensions;

namespace ThreePillers.AddressBook.Application.UseCases.AddressBookEntries.Commands.Login;

public sealed class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator(
        IAddressBookEntryRepository addressBookEntryRepository,
        IPasswordHasher<AddressBookEntry> passwordHasher
    )
    {
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Email is required")
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email address not valid")
            .Must(email => email.IsValidEmail())
            .WithMessage("Email address not valid");

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Password is required")
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must have at least 6 chars")
            .MaximumLength(24)
            .WithMessage("Password must have at max 24 chars");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await addressBookEntryRepository.AnyAsync(x =>
                        x.Email.ToLower() == req.Email.ToLower()
                    );
                }
            )
            .WithMessage("Email not registerd, please go to sign up first");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var addressBookEntry = await addressBookEntryRepository.GetByEmailForLoginAsync(
                        req.Email,
                        ct
                    );

                    var result = passwordHasher.VerifyHashedPassword(
                        addressBookEntry,
                        addressBookEntry.HashedPassword,
                        req.Password
                    );
                    return result == PasswordVerificationResult.Success
                        || result == PasswordVerificationResult.SuccessRehashNeeded;
                }
            )
            .WithMessage(
                "Your credential is not correct, please try again with correct credentials"
            );
    }
}
