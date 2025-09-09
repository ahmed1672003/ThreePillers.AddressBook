namespace ThreePillers.AddressBook.Application.UseCases.Departments.Queries.GetById;

public sealed class GetDepartmentByIdValidator : AbstractValidator<GetDepartmentByIdQuery>
{
    public GetDepartmentByIdValidator(IDepartmentRepository departmentRepository)
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
