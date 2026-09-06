using Microsoft.Extensions.DependencyInjection;
using Mizan.Application.Common.Abstractions.Messaging.Queries;

namespace Mizan.Application.Common.Services;

public sealed class QueryExecutor : IQueryExecutor
{
    private readonly IServiceProvider _serviceProvider;

    public QueryExecutor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResult> ExecuteAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider
            .GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return handler.Handle(query, cancellationToken);
    }
}