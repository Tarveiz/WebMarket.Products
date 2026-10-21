using Microsoft.Extensions.Configuration;

namespace WebMarket.Products.Common.Helpers.Extensions;

public static class ConfigurationBuilderExtensions
{
    private const string VaultSecretsDirectory = "/vault/secrets";

    /// <summary>
    /// Подключает JSON-файлы секретов, отрендеренные Vault Agent.
    /// </summary>
    public static IConfigurationBuilder AddVaultSecrets(this IConfigurationBuilder builder, string directory = VaultSecretsDirectory)
    {
        if (!Directory.Exists(directory))
            return builder;

        foreach (var file in Directory.EnumerateFiles(directory, "*.json").Order())
            builder.AddJsonFile(file, optional: true, reloadOnChange: true);

        return builder;
    }
}
