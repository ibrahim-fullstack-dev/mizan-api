using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Tenants.DeactivateTenant;

public sealed class DeactivateTenantHandler: ICommandHandler<DeactivateTenantCommand, Unit>
{
    private readonly IPlatformDbContext _context;

    public DeactivateTenantHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeactivateTenantCommand command,
        CancellationToken cancellationToken = default)
    {
        var tenant = await _context.Tenants
            .FirstOrDefaultAsync(
                tenant => tenant.Id == command.TenantId,
                cancellationToken);

        if (tenant is null)
            throw new KeyNotFoundException(
                $"Tenant with ID {command.TenantId} was not found.");

        tenant.Deactivate();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
