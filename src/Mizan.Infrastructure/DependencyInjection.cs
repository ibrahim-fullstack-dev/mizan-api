using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mizan.Application.Common.Interfaces;
using Mizan.Infrastructure.Platform.Persistence;

namespace Mizan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Database connection string 'DefaultConnection' is not configured.");
        }

        services.AddDbContext<MizanPlatformDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IPlatformDbContext>(
            serviceProvider => serviceProvider.GetRequiredService<MizanPlatformDbContext>());

        return services;
    }
}
