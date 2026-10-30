using WebMarket.Products.Common.Cqrs.Abstractions;
using WebMarket.Products.Common.Responses.Products;

namespace WebMarket.Products.Common.Cqrs.Queries.Products;

/// <summary>
/// Получение опубликованного товара
/// </summary>
public sealed record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>;
