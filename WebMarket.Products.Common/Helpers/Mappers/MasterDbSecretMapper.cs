using System.Reflection;
using Npgsql;
using WebMarket.Products.Common.Settings.Secrets;

namespace WebMarket.Products.Common.Helpers.Mappers;

public static class MasterDbSecretMapper
{
    /// <summary>
    /// Собирает строку подключения к PostgreSQL из секрета.
    /// </summary>
    public static string ToConnectionString(this MasterDbSecret secret) => new NpgsqlConnectionStringBuilder
    {
        Host = secret.Host,
        Port = secret.Port,
        Database = secret.Database,
        Username = secret.Username,
        Password = secret.Password,
        ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name
    }.ConnectionString;
}
