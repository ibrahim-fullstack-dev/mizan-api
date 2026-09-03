using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Interfaces;
using Mizan.Domain.Platform.Tenants;

namespace Mizan.Application.Platform.Tenants.GetTenant;

public sealed class GetTenantHandler
{
    private readonly IPlatformDbContext _context;

    public GetTenantHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<Tenant?> Handle(
        GetTenantQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _context.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(
                tenant => tenant.Id == query.TenantId,
                cancellationToken);
    }
}
