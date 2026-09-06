using Microsoft.EntityFrameworkCore;
using Mizan.Application.Common.Abstractions.Messaging.Queries;
using Mizan.Application.Common.Interfaces;
using Mizan.Application.Platform.Tenants.DTOs;

namespace Mizan.Application.Platform.Tenants.GetTenant;

public sealed class GetTenantHandler
    : IQueryHandler<GetTenantQuery, TenantDto>
{
    private readonly IPlatformDbContext _context;

    public GetTenantHandler(IPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<TenantDto> Handle(
        GetTenantQuery query,
        CancellationToken cancellationToken = default)
    {
        var tenant = await _context.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(
                tenant => tenant.Id == query.TenantId,
                cancellationToken);

        if (tenant is null)
        {
            throw new KeyNotFoundException(
                $"Tenant with ID '{query.TenantId}' was not found.");
        }

        return new TenantDto(
            tenant.Id,
            tenant.Name,
            tenant.SubDomain,
            tenant.SchemaName,
            tenant.Status.ToString());
    }
}