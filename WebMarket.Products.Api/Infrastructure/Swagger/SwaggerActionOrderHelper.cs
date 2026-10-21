using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebMarket.Products.Api.Infrastructure.Swagger;

/// <summary>
/// Порядок отображения контроллеров в Swagger UI.
/// </summary>
internal static class SwaggerActionOrderHelper
{
    private static readonly string[] TagOrder = ["Products", "Owners", "Categories"];

    /// <summary>
    /// Ключ сортировки операций: сначала по позиции контроллера в TagOrder, затем по имени и маршруту.
    /// </summary>
    public static string GetSortKey(ApiDescription api)
    {
        var controller = api.ActionDescriptor.RouteValues["controller"] ?? string.Empty;
        var index = Array.IndexOf(TagOrder, controller);
        return $"{(index >= 0 ? index : TagOrder.Length):D3}_{controller}_{api.RelativePath}_{api.HttpMethod}";
    }
}
