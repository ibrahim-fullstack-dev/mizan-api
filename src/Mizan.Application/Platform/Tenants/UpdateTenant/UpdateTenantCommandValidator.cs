using FluentValidation;

namespace Mizan.Application.Platform.Tenants.UpdateTenant;

public sealed class UpdateTenantCommandValidator
    : AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantCommandValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0);

        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.SubDomain)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.SchemaName)
            .NotEmpty()
            .MaximumLength(100);
    }
}