namespace Mizan.Application.Common.Abstractions.Messaging.Queries;

public interface IQueryExecutor
{
    Task<TResult> ExecuteAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResult>;
}