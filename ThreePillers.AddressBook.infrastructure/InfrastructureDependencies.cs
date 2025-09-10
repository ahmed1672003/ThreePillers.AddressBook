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
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ISupabaseStorage, SupabaseStorage>(cfg =>
        {
            var supabaseClient = new Client(
                SupabaseSettings.SupabaseUrl,
                new ClientOptions() { HttpUploadTimeout = TimeSpan.FromMinutes(5) },
                new() { { "Authorization", SupabaseSettings.SupabaseKey } }
            );
            return new SupabaseStorage(supabaseClient);
        });
        return services;
    }
}
