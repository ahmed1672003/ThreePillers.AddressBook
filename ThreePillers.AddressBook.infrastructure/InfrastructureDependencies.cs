using ThreePillers.AddressBook.Domain.Entities.Jobs;

namespace ThreePillers.AddressBook.infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection RegisterInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<AddressBookDbContext>(cfg =>
            cfg.UseSqlServer(configuration.GetConnectionString("DbConnection"))
        );
        services
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IJobRepository, JobRepository>()
            .AddScoped<IDepartmentRepository, DepartmentRepository>();
        return services;
    }
}
