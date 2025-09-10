namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Queries.GetById;

public sealed class GetJobByIdValidator : AbstractValidator<GetJobByIdQuery>
{
    public GetJobByIdValidator(IJobRepository jobRepository)
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
