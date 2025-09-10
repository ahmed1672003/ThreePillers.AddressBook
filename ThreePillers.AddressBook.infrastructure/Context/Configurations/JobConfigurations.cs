namespace ThreePillers.AddressBook.infrastructure.Context.Configurations;

internal sealed class JobConfigurations : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.HasIndex(x => x.Title).IsUnique(true);
    }
}
