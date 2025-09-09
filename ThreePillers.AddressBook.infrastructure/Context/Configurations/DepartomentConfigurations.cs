namespace ThreePillers.AddressBook.infrastructure.Context.Configurations;

internal sealed class DepartomentConfigurations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.HasIndex(x => x.Name).IsUnique(true);
    }
}
