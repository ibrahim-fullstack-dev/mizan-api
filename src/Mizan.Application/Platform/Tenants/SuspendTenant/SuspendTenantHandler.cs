using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Tenants.SuspendTenant;

public sealed class SuspendTenantHandler: ICommandHandler<SuspendTenantCommand,Unit>
{
    private readonly IPlatformDbContext _context;

    public SuspendTenantHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        SuspendTenantCommand command,
        CancellationToken cancellationToken = default)
    {
        var tenant = await _context.Tenants
            .FirstOrDefaultAsync(
                tenant => tenant.Id == command.TenantId,
                cancellationToken);

        if (tenant is null)
            throw new KeyNotFoundException(
                $"Tenant with ID {command.TenantId} was not found.");

        tenant.Suspend();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
