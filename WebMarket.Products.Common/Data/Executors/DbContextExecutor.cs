using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace WebMarket.Products.Common.Data.Executors;

/// <summary>
/// Dapper в соединении и транзакции DbContext
/// </summary>
internal sealed class DbContextExecutor(DbContext context) : DbExecutorBase
{
    protected override Task<TResult> RunAsync<TResult>(Func<NpgsqlConnection, NpgsqlTransaction?, Task<TResult>> command, CancellationToken ct)
    {
        var connection = (NpgsqlConnection)context.Database.GetDbConnection();
        var transaction = (NpgsqlTransaction?)context.Database.CurrentTransaction?.GetDbTransaction();
        return command(connection, transaction); // Dapper сам откроет закрытое соединение
    }
}
