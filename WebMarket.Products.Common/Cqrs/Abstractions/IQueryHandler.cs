namespace WebMarket.Products.Common.Cqrs.Abstractions;

/// <summary>
/// Обработчик запроса чтения.
/// </summary>
public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken ct = default);
}
