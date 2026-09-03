using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Platform.Plans.DeactivatePlan;

public sealed record DeactivatePlanCommand(int PlanId): ICommand<Unit>;