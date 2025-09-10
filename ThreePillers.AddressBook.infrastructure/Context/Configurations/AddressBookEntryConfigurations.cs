namespace ThreePillers.AddressBook.infrastructure.Context.Configurations;

internal sealed class AddressBookEntryConfigurations : IEntityTypeConfiguration<AddressBookEntry>
{
    public void Configure(EntityTypeBuilder<AddressBookEntry> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique(true);
        builder.HasIndex(x => x.Phone).IsUnique(true);
        builder.Ignore(x => x.Age);
    }
}
