using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Mizan.Application.Common.Interfaces;
using Mizan.Infrastructure.Platform.Persistence;

namespace Mizan.Infrastructure;

// Register Infrastructure services in the DI container.
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Read the database connection string.
        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        // Stop startup if the connection string is missing.
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Database connection string 'DefaultConnection' is not configured.");
        }

        // Register the Platform DbContext with PostgreSQL.
        services.AddDbContext<MizanPlatformDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Link the Application abstraction to the Infrastructure implementation.
        services.AddScoped<IPlatformDbContext>(
            serviceProvider =>
                serviceProvider.GetRequiredService<MizanPlatformDbContext>());

        return services;
    }
}