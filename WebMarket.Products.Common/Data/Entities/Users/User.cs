using WebMarket.Products.Common.Data.Entities.Products;

namespace WebMarket.Products.Common.Data.Entities.Users;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    public Guid Id { get; set; }

    public required string Email { get; set; }

    public required string DisplayName { get; set; }

    public DateTime CreatedAt { get; set; }

    // Навигационные свойства
    public ICollection<Product> Products { get; set; } = [];
}
