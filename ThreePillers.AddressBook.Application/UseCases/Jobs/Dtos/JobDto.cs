namespace ThreePillers.AddressBook.Application.UseCases.Jobs.Dtos;

public sealed record JobDto
{
    public long Id { get; set; }
    public string Title { get; set; }
}
