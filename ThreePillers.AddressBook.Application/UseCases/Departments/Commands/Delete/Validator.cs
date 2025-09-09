namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Delete;

public sealed class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentValidator(IDepartmentRepository departmentRepository)
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
    }
}
