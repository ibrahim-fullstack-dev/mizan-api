using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Tenants.ReactivateTenant;

public sealed class ReactivateTenantHandler: ICommandHandler<ReactivateTenantCommand,Unit>
{
    private readonly IPlatformDbContext _context;

    public ReactivateTenantHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        ReactivateTenantCommand command,
        CancellationToken cancellationToken = default)
    {
        var tenant = await _context.Tenants
            .FirstOrDefaultAsync(
                tenant => tenant.Id == command.TenantId,
                cancellationToken);

        if (tenant is null)
            throw new KeyNotFoundException(
                $"Tenant with ID {command.TenantId} was not found.");

        tenant.Reactivate();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
