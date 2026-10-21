using WebMarket.Products.Common.Data.Entities.Products.Enums;
using WebMarket.Products.Common.Data.Entities.Users;

namespace WebMarket.Products.Common.Data.Entities.Products;

/// <summary>
/// Товар.
/// </summary>
public class Product
{
    public Guid Id { get; set; }

    public required string Sku { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public ProductStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public uint Version { get; set; }

    // Навигационные свойства
    public User CreatedBy { get; set; } = null!;
}
