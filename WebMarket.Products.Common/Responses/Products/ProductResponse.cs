using System.Text.Json.Serialization;
using WebMarket.Products.Common.Helpers.Converters;

namespace WebMarket.Products.Common.Responses.Products;

/// <summary>
/// Карточка товара
/// </summary>
public sealed class ProductResponse
{
    public Guid Id { get; init; }

    public Guid OwnerId { get; init; }

    public Guid CategoryId { get; init; }

    public required string Sku { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public decimal Price { get; init; }

    public required string Currency { get; init; }

    public int AvailableStock { get; init; }

    [JsonConverter(typeof(RawJsonConverter))]
    public required string Attributes { get; init; }
}
