using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Services;

namespace Mizan.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly);

        services.AddScoped<IValidationService, ValidationService>();

        services.AddScoped<ICommandExecutor, CommandExecutor>();

        RegisterCommandHandlers(services);

        return services;
    }

    private static void RegisterCommandHandlers(
        IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        var handlerTypes = assembly
            .GetTypes()
            .Where(type =>
                type is { IsClass: true, IsAbstract: false } &&
                type.GetInterfaces()
                    .Any(interfaceType =>
                        interfaceType.IsGenericType &&
                        interfaceType.GetGenericTypeDefinition() ==
                        typeof(ICommandHandler<,>)));

        foreach (var handlerType in handlerTypes)
        {
            var handlerInterface = handlerType
                .GetInterfaces()
                .Single(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() ==
                    typeof(ICommandHandler<,>));

            services.AddScoped(handlerInterface, handlerType);
        }
    }
}