using ThreePillers.AddressBook.Application.Middlewares;

namespace ThreePillers.AddressBook.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder
            .Services.RegisterApplicationDependencies()
            .RegisterInfrastructureDependencies(builder.Configuration);
        builder.Services.AddScoped<ExceptionHandler>();
        var app = builder.Build();
        app.UseMiddleware<ExceptionHandler>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
