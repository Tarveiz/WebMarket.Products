using Microsoft.EntityFrameworkCore;
using WebMarket.Products.Common.Data.Executors;

namespace WebMarket.Products.Common.Helpers.Extensions;

public static class DbContextExtensions
{
    /// <summary>
    /// Dapper в соединении и транзакции контекста
    /// </summary>
    public static IDbExecutor GetDbExecutor(this DbContext context) => new DbContextExecutor(context);
}
