namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Create;

public sealed class CreateJobValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobValidator(IJobRepository jobRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Title)
            .NotNull()
            .WithMessage("Job title is required")
            .NotEmpty()
            .WithMessage("Job title is required")
            .MaximumLength(100)
            .WithMessage("Job title must not exceed 2 characters")
            .MinimumLength(2)
            .WithMessage("Job title must exceed at least 2 characters");

        RuleFor(x => x)
            .MustAsync(
                async (command, ct) =>
                {
                    return !await jobRepository.AnyAsync(
                        x => x.Title.ToLower() == command.Title.ToLower(),
                        ct
                    );
                }
            )
            .WithMessage("Job title used once.");
    }
}
