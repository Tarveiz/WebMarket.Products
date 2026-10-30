using Microsoft.AspNetCore.Mvc;
using WebMarket.Products.Common.Cqrs.Abstractions;
using WebMarket.Products.Common.Cqrs.Queries.Products;
using WebMarket.Products.Common.Helpers.Validators;
using WebMarket.Products.Common.Responses.Products;
using WebMarket.Products.Common.Settings.Constants;

namespace WebMarket.Products.Api.Controllers;

[ApiController]
[Route(BaseConstants.ApiEntryPoint)]
public class ProductsController : ControllerBase
{
    [HttpGet("Products/{productId}")]
    [ProducesResponseType<ProductResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductResponse>> GetProductAsync(string productId, [FromServices] IQueryHandler<GetProductQuery, ProductResponse> handler, CancellationToken ct = default)
    {
        var parsedProductId = RequestValidationHelper.ParseRequiredId(productId, "Product ID must be a valid GUID.", ErrorCodes.Products.InvalidProductId);
        return Ok(await handler.HandleAsync(new GetProductQuery(parsedProductId), ct));
    }
}
