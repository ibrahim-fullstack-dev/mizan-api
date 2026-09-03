using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Tenants.UpdateTenant;

public sealed record UpdateTenantCommand(
    int Id,
    string Name,
    string SubDomain,
    string SchemaName)
    : ICommand<Unit>;