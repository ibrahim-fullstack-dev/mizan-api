using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

using Mizan.Application.Common.Abstractions.Messaging.Commands;
using Mizan.Application.Common.Abstractions.Messaging.Queries;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Services;

namespace Mizan.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Register all validators in the Application assembly.
        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly);

        // Register Application services.
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<ICommandExecutor, CommandExecutor>();
        services.AddScoped<IQueryExecutor, QueryExecutor>();

        // Automatically register all command and query handlers.
        RegisterHandlers(services);

        return services;
    }

    private static void RegisterHandlers(
        IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // Find all concrete classes that implement a handler interface.
        var handlerTypes = assembly.GetTypes()
            .Where(type =>
                type is { IsClass: true, IsAbstract: false } &&
                type.GetInterfaces().Any(IsHandlerInterface));

        foreach (var handlerType in handlerTypes)
        {
            // Get the ICommandHandler<,> or IQueryHandler<,> interface.
            var handlerInterface = handlerType.GetInterfaces()
                .Single(IsHandlerInterface);

            services.AddScoped(handlerInterface, handlerType);
        }
    }

    private static bool IsHandlerInterface(Type type)
    {
        if (!type.IsGenericType)
            return false;

        var genericType = type.GetGenericTypeDefinition();

        return genericType == typeof(ICommandHandler<,>) ||
               genericType == typeof(IQueryHandler<,>);
    }
}