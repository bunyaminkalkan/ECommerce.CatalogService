using ECommerce.CatalogService.API.UseCases.Commands.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Space.Abstraction;

namespace ECommerce.CatalogService.API.Controllers;

[ApiController]
[Route("/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminProductController(ISpace space) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand request)
    {
        var response = await space.Send(request);
        return Ok(response);
    }

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid productId, [FromBody] UpdateProductCommand request)
    {
        request.Id = productId;
        var response = await space.Send(request);
        return Ok(response);
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid productId)
    {
        var request = new DeleteProductCommand(productId);
        var response = await space.Send(request);
        return Ok(response);
    }
}
