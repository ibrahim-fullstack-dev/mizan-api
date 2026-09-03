using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Application.Common.Interfaces;

namespace Mizan.Application.Platform.Tenants.UpdateTenant;

public sealed class UpdateTenantHandler: ICommandHandler<UpdateTenantCommand,Unit>
{
    private readonly IPlatformDbContext _context;

    public UpdateTenantHandler(IPlatformDbContext context)
    {
        _context = context;
    }

public async Task<Unit> Handle(
    UpdateTenantCommand command,
    CancellationToken cancellationToken = default)
{
    var tenant = await _context.Tenants
        .FirstOrDefaultAsync(
            tenant => tenant.Id == command.Id,
            cancellationToken);

    if (tenant is null)
        throw new KeyNotFoundException( 
            $"Tenant with ID {command.Id} was not found.");

    tenant.UpdateDetails(
        command.Name,
        command.SubDomain);

    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
}
}
