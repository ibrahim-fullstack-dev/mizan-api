using Mizan.Application.Common.Abstractions.Messaging.Commands;

namespace Mizan.Application.Platform.Storage.UpdateStorageUsage;

public sealed record UpdateStorageUsageCommand(
    int TenantId,
    long UsedBytes
) : ICommand<Unit>;