using Mizan.Application.Common.Abstractions.Messaging;
namespace Mizan.Application.Platform.Plans.ActivatePlan;

public sealed record ActivatePlanCommand(int PlanId): ICommand<Unit>;