namespace WebMarket.Products.Common.Sql;

/// <summary>
/// SQL-запросы по товарам
/// </summary>
public static class ProductSql
{
    public const string GetPublishedById = """
        SELECT id, owner_id, category_id, sku, name, description, price, currency, stock - reserved_stock AS available_stock, attributes
        FROM catalog.products
        WHERE id = @ProductId AND status = @Status AND NOT is_deleted
        """;
}
