namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Commands.Delete;

public sealed class DeleteJobValidator : AbstractValidator<DeleteJobCommand>
{
    public DeleteJobValidator(IJobRepository jobRepository)
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
    }
}
