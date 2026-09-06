using Mizan.Application.Common.Abstractions.Messaging.Queries;
using Mizan.Application.Platform.Tenants.DTOs;

namespace Mizan.Application.Platform.Tenants.GetTenant;

public sealed record GetTenantQuery(int TenantId)
    : IQuery<TenantDto>;