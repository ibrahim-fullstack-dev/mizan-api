using Mizan.Application.Common.Abstractions.Messaging.Commands;

namespace Mizan.Application.Platform.Tenants.CreateTenant;

public sealed record CreateTenantCommand(
    string Name,
    string SubDomain) : ICommand<int>;
