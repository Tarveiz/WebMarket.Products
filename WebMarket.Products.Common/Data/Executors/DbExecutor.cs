using Npgsql;

namespace WebMarket.Products.Common.Data.Executors;

/// <summary>
/// Dapper на соединении из пула
/// </summary>
internal sealed class DbExecutor(NpgsqlDataSource dataSource) : DbExecutorBase
{
    protected override async Task<TResult> RunAsync<TResult>(Func<NpgsqlConnection, NpgsqlTransaction?, Task<TResult>> command, CancellationToken ct)
    {
        await using var connection = await dataSource.OpenConnectionAsync(ct);
        return await command(connection, null);
    }
}
