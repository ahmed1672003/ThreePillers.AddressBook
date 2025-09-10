namespace ThreePillers.AddressBook.Application.UseCases.Stream.Commands.Upload;

public sealed class UploadStreamValidator : AbstractValidator<UploadStreamCommand>
{
    public UploadStreamValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x)
            .Must(command =>
            {
                var photoExtensions = Enum.GetNames(typeof(Domain.Enums.PhotoExtension))
                    .Select(x => x.ToLower())
                    .ToList();
                var fileExtension = Path.GetExtension(command.File.FileName);
                return photoExtensions.Contains(fileExtension.TrimStart('.').ToLower());
            })
            .WithMessage("File extension not supported");
    }
}
