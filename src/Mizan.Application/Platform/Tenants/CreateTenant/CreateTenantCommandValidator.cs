using FluentValidation;

namespace Mizan.Application.Platform.Tenants.CreateTenant;

public sealed class CreateTenantCommandValidator
    : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.SubDomain)
            .NotEmpty()
            .MaximumLength(100);

    }
}
