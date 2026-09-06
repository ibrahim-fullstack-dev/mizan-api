using Mizan.Application.Common.Abstractions.Messaging.Commands;

namespace Mizan.Application.Platform.Tenants.ReactivateTenant;

public sealed record ReactivateTenantCommand(int TenantId) : ICommand<Unit>;