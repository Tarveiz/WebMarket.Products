using WebMarket.DbProvider.Entities.Products.Enums;
using WebMarket.DbProvider.Executors;
using WebMarket.Products.Common.Cqrs.Abstractions;
using WebMarket.Products.Common.Exceptions;
using WebMarket.Products.Common.Responses.Products;
using WebMarket.Products.Common.Settings.Constants;
using WebMarket.Products.Common.Sql;

namespace WebMarket.Products.Common.Cqrs.Queries.Products;

/// <summary>
/// Обработчик получения опубликованного товара
/// </summary>
public sealed class GetProductQueryHandler(IDbExecutor dbExecutor) : IQueryHandler<GetProductQuery, ProductResponse>
{
    public async Task<ProductResponse> HandleAsync(GetProductQuery query, CancellationToken ct = default)
    {
        return await dbExecutor.QueryFirstOrDefaultAsync<ProductResponse>(ProductSql.GetPublishedById, new { query.ProductId, Status = ProductStatus.Published }, ct)
            ?? throw new NotFoundException("Product not found.", ErrorCodes.Products.ProductNotFound);
    }
}
