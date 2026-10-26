using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using Dapper;

namespace WebMarket.Products.Common.Data.Executors;

/// <summary>
/// Сборка SQL вызова функций и процедур
/// </summary>
internal static partial class DbRoutineSqlBuilder
{
    public const string Function = "SELECT * FROM {0}({1})";
    public const string Procedure = "CALL {0}({1})";

    private static readonly ConcurrentDictionary<(string Format, string Name, Type ParametersType), string> Cache = new();

    public static string Build(string format, string routineName, object? parameters) => parameters is DynamicParameters dynamicParameters
        ? Compose(format, routineName, dynamicParameters.ParameterNames)
        : Cache.GetOrAdd((format, routineName, parameters?.GetType() ?? typeof(void)), static key => Compose(key.Format, key.Name, key.ParametersType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(property => property.Name)));

    private static string Compose(string format, string routineName, IEnumerable<string> parameterNames)
    {
        var arguments = parameterNames.Select(name => $"{EnsureIdentifier(JsonNamingPolicy.SnakeCaseLower.ConvertName(name))} => @{name}");
        return string.Format(format, EnsureIdentifier(routineName), string.Join(", ", arguments));
    }

    private static string EnsureIdentifier(string identifier) => IdentifierRegex().IsMatch(identifier) ? identifier : throw new ArgumentException($"Invalid SQL identifier '{identifier}'.", nameof(identifier));

    [GeneratedRegex(@"^[a-z_][a-z0-9_]*(\.[a-z_][a-z0-9_]*)?$")]
    private static partial Regex IdentifierRegex();
}
