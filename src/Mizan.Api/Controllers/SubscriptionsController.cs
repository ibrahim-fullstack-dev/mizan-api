using Microsoft.AspNetCore.Mvc;
using Mizan.Application.Common.Abstractions.Messaging;
using Mizan.Application.Platform.Subscriptions.CancelSubscription;
using Mizan.Application.Platform.Subscriptions.CreateSubscription;
using Mizan.Application.Platform.Subscriptions.ExpireSubscription;
using Mizan.Application.Platform.Subscriptions.ReactivateSubscription;
using Mizan.Application.Platform.Subscriptions.RenewSubscription;

namespace Mizan.Api.Controllers;

[ApiController]
[Route("api/subscriptions")]
public sealed class SubscriptionsController : ControllerBase
{
    private readonly ICommandExecutor _commandExecutor;

    public SubscriptionsController(
        ICommandExecutor commandExecutor)
    {
        _commandExecutor = commandExecutor;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        var subscriptionId =
            await _commandExecutor.ExecuteAsync<
                CreateSubscriptionCommand,
                int>(
                command,
                cancellationToken);

        return Ok(subscriptionId);
    }

    [HttpPost("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new CancelSubscriptionCommand(id);

        await _commandExecutor.ExecuteAsync<
            CancelSubscriptionCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:int}/reactivate")]
    public async Task<IActionResult> Reactivate(
        int id,
        CancellationToken cancellationToken)
    {
        var command =
            new ReactivateSubscriptionCommand(id);

        await _commandExecutor.ExecuteAsync<
            ReactivateSubscriptionCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:int}/expire")]
    public async Task<IActionResult> Expire(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new ExpireSubscriptionCommand(id);

        await _commandExecutor.ExecuteAsync<
            ExpireSubscriptionCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:int}/renew")]
    public async Task<IActionResult> Renew(
        int id,
        RenewSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.SubscriptionId)
            return BadRequest(
                "Route ID does not match command subscription ID.");

        await _commandExecutor.ExecuteAsync<
            RenewSubscriptionCommand,
            Unit>(
            command,
            cancellationToken);

        return NoContent();
    }
}