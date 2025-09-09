namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Update;

public sealed class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentValidator(IDepartmentRepository departmentRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .MustAsync(
                async (command, ct) =>
                {
                    return await departmentRepository.AnyAsync(x => x.Id == command.Id, ct);
                }
            )
            .WithMessage("Department not exist");

        RuleFor(x => x)
            .MustAsync(
                async (command, ct) =>
                {
                    return !await departmentRepository.AnyAsync(
                        x => x.Id != command.Id && x.Name.ToLower() == command.Name.ToLower(),
                        ct
                    );
                }
            )
            .WithMessage("Department name used once");
    }
}
