using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Tenants.DeactivateTenant;

public sealed record DeactivateTenantCommand(int TenantId): ICommand<Unit>;