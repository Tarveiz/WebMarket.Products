using Dapper;
using Npgsql;
using static WebMarket.Products.Common.Data.Executors.DbRoutineSqlBuilder;

namespace WebMarket.Products.Common.Data.Executors;

/// <summary>
/// Базовая реализация IDbExecutor
/// </summary>
internal abstract class DbExecutorBase : IDbExecutor
{
    public Task<IReadOnlyList<T>> QueryFunctionAsync<T>(string functionName, object? parameters = null, CancellationToken ct = default) => QueryAsync<T>(Build(Function, functionName, parameters), parameters, ct);

    public Task<T?> QueryFunctionFirstOrDefaultAsync<T>(string functionName, object? parameters = null, CancellationToken ct = default) => QueryFirstOrDefaultAsync<T>(Build(Function, functionName, parameters), parameters, ct);

    public Task CallProcedureAsync(string procedureName, object? parameters = null, CancellationToken ct = default) => ExecuteAsync(Build(Procedure, procedureName, parameters), parameters, ct);

    public Task<T?> CallProcedureAsync<T>(string procedureName, object? parameters = null, CancellationToken ct = default) => QueryFirstOrDefaultAsync<T>(Build(Procedure, procedureName, parameters), parameters, ct);

    public Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? parameters = null, CancellationToken ct = default) => RunAsync<IReadOnlyList<T>>(async (connection, transaction) => (await connection.QueryAsync<T>(CreateCommand(sql, parameters, transaction, ct))).AsList(), ct);

    public Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, CancellationToken ct = default) => RunAsync((connection, transaction) => connection.QueryFirstOrDefaultAsync<T>(CreateCommand(sql, parameters, transaction, ct)), ct);

    public Task<int> ExecuteAsync(string sql, object? parameters = null, CancellationToken ct = default) => RunAsync((connection, transaction) => connection.ExecuteAsync(CreateCommand(sql, parameters, transaction, ct)), ct);

    protected abstract Task<TResult> RunAsync<TResult>(Func<NpgsqlConnection, NpgsqlTransaction?, Task<TResult>> command, CancellationToken ct);

    private static CommandDefinition CreateCommand(string sql, object? parameters, NpgsqlTransaction? transaction, CancellationToken ct) => new(sql, parameters, transaction, cancellationToken: ct);
}
