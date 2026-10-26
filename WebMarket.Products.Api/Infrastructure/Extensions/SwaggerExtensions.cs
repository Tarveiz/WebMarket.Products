using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;

namespace WebMarket.Products.Api.Infrastructure.Extensions;

internal static class SwaggerExtensions
{
    private const string DocumentName = "v1";
    private const string Title = "WebMarket Products API";
    private static readonly string[] TagOrder = ["Products", "Owners", "Categories"];

    /// <summary>
    /// Регистрирует Swagger с JWT-авторизацией
    /// </summary>
    public static IServiceCollection AddWebMarketSwagger(this IServiceCollection services) => services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(DocumentName, new OpenApiInfo { Title = Title, Version = DocumentName });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "JWT access token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        options.AddSecurityRequirement(document => new OpenApiSecurityRequirement { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });
        options.SupportNonNullableReferenceTypes();
        options.EnableAnnotations();
        options.OrderActionsBy(GetSortKey);
    });

    /// <summary>
    /// Подключает Swagger UI вне Production
    /// </summary>
    public static void UseWebMarketSwagger(this WebApplication app)
    {
        if (app.Environment.IsProduction())
            return;

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{DocumentName}/swagger.json", $"{Title} {DocumentName}");
            options.DisplayRequestDuration();
            options.EnablePersistAuthorization();
        });
    }

    /// <summary>
    /// Ключ сортировки операций: сначала по позиции контроллера в TagOrder, затем по имени и маршруту.
    /// </summary>
    private static string GetSortKey(ApiDescription api)
    {
        var controller = api.ActionDescriptor.RouteValues["controller"] ?? string.Empty;
        var index = Array.IndexOf(TagOrder, controller);
        return $"{(index >= 0 ? index : TagOrder.Length):D3}_{controller}_{api.RelativePath}_{api.HttpMethod}";
    }
}
