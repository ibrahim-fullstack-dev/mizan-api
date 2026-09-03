using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mizan.Application.Common.Interfaces;

namespace Mizan.Application.Common.Services;

public sealed class ValidationService : IValidationService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateAsync<T>(
        T instance,
        CancellationToken cancellationToken = default)
    {
        var validator = _serviceProvider
            .GetService<IValidator<T>>();

        if (validator is null)
            return;

        await validator.ValidateAndThrowAsync(
            instance,
            cancellationToken);
    }
}