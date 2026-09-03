using Microsoft.Extensions.DependencyInjection;
using Mizan.Application.Common.Abstractions.Messaging;

namespace Mizan.Application.Common.Services;

public sealed class CommandExecutor : ICommandExecutor
{
    private readonly IServiceProvider _serviceProvider;

    public CommandExecutor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResult> ExecuteAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResult>
    {
        var handler = _serviceProvider
            .GetRequiredService<ICommandHandler<TCommand, TResult>>();

        return handler.Handle(command, cancellationToken);
    }
}
