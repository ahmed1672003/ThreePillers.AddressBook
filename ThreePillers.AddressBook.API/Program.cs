using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http.Features;
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
        builder.Services.AddRateLimiter(options =>
        {
            options.AddPolicy(
                "PublicStream",
                httpContext =>
                {
                    var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();

                    return RateLimitPartition.GetFixedWindowLimiter(
                        ipAddress,
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(1),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        }
                    );
                }
            );

            options.RejectionStatusCode = 429;
        });
        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 5 * 1024 * 1024;
        });
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Limits.MaxRequestBodySize = 5 * 1024 * 1024;
        });
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
