using WebMarket.Products.Common.Cqrs.Abstractions;
using WebMarket.Products.Common.Data;

namespace WebMarket.Products.Common.Cqrs;

/// <summary>
/// Базовый обработчик запроса чтения.
/// </summary>
public abstract class QueryHandler<TQuery, TResult>(WebMarketDbContext context) : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    protected WebMarketDbContext Context { get; } = context;

    public abstract Task<TResult> HandleAsync(TQuery query, CancellationToken ct = default);
}
