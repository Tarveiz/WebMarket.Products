namespace WebMarket.Products.Common.Settings.Secrets;

/// <summary>
/// Секрет подключения к основной БД PostgreSQL.
/// </summary>
public sealed class MasterDbSecret
{
    public required string Host { get; init; }

    public required int Port { get; init; }

    public required string Database { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }
}
