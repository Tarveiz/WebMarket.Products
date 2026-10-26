namespace WebMarket.Products.Common.Data.Executors;

/// <summary>
/// Доступ к PostgreSQL через Dapper
/// </summary>
public interface IDbExecutor
{
    /// <summary>
    /// Вызывает функцию и возвращает набор строк
    /// </summary>
    Task<IReadOnlyList<T>> QueryFunctionAsync<T>(string functionName, object? parameters = null, CancellationToken ct = default);

    /// <summary>
    /// Вызывает функцию и возвращает первую строку или default
    /// </summary>
    Task<T?> QueryFunctionFirstOrDefaultAsync<T>(string functionName, object? parameters = null, CancellationToken ct = default);

    /// <summary>
    /// Вызывает процедуру
    /// </summary>
    Task CallProcedureAsync(string procedureName, object? parameters = null, CancellationToken ct = default);

    /// <summary>
    /// Вызывает процедуру и возвращает out-параметры
    /// </summary>
    Task<T?> CallProcedureAsync<T>(string procedureName, object? parameters = null, CancellationToken ct = default);

    /// <summary>
    /// Выполняет SQL и возвращает набор строк
    /// </summary>
    Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? parameters = null, CancellationToken ct = default);

    /// <summary>
    /// Выполняет SQL и возвращает первую строку или default
    /// </summary>
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, CancellationToken ct = default);

    /// <summary>
    /// Выполняет SQL и возвращает число затронутых строк
    /// </summary>
    Task<int> ExecuteAsync(string sql, object? parameters = null, CancellationToken ct = default);
}
