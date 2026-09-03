using FluentValidation;

namespace Mizan.Application.Platform.Storage.UpdateStorageUsage;

public sealed class UpdateStorageUsageCommandValidator
    : AbstractValidator<UpdateStorageUsageCommand>
{
    public UpdateStorageUsageCommandValidator()
    {
        RuleFor(command => command.TenantId)
            .GreaterThan(0);

        RuleFor(command => command.UsedBytes)
            .GreaterThanOrEqualTo(0);
    }
}