namespace ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Create;

public sealed class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentValidator(IDepartmentRepository departmentRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Department name is required")
            .NotEmpty()
            .WithMessage("Department name is required")
            .MaximumLength(100)
            .WithMessage("Department name must not exceed 2 characters")
            .MinimumLength(2)
            .WithMessage("Department name must exceed at least 2 characters");

        RuleFor(x => x)
            .MustAsync(
                async (command, ct) =>
                {
                    return !await departmentRepository.AnyAsync(
                        x => x.Name.ToLower() == command.Name.ToLower(),
                        ct
                    );
                }
            )
            .WithMessage("Department name used once.");
    }
}
