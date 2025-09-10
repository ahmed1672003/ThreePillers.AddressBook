namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Update;

public sealed class UpdateJobValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobValidator(IJobRepository jobRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .MustAsync(
                async (command, ct) =>
                {
                    return await jobRepository.AnyAsync(x => x.Id == command.Id, ct);
                }
            )
            .WithMessage("Job not exist");

        RuleFor(x => x)
            .MustAsync(
                async (command, ct) =>
                {
                    return !await jobRepository.AnyAsync(
                        x => x.Id != command.Id && x.Title.ToLower() == command.Title.ToLower(),
                        ct
                    );
                }
            )
            .WithMessage("Job title used once");
    }
}
