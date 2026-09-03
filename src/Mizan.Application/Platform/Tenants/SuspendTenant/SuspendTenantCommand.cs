using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Tenants.SuspendTenant;

public sealed record SuspendTenantCommand(int TenantId): ICommand<Unit>;