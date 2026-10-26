using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using WebMarket.Products.Common.Data;
using WebMarket.Products.Common.Data.Executors;
using WebMarket.Products.Common.Helpers.Mappers;
using WebMarket.Products.Common.Settings.Secrets;

namespace WebMarket.Products.Common.Helpers.Extensions;

public static class DataServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует доступ к БД через EF Core и Dapper
    /// </summary>
    public static IServiceCollection AddWebMarketData(this IServiceCollection services, MasterDbSecret masterDbSecret)
    {
        services.AddSingleton(_ => NpgsqlDataSource.Create(masterDbSecret.ToConnectionString()));

        services.AddDbContextPool<WebMarketDbContext>((provider, options) => options.UseNpgsql(provider.GetRequiredService<NpgsqlDataSource>()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddSingleton<IDbExecutor, DbExecutor>();

        return services;
    }
}
