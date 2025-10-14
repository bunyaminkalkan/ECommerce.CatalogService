using ECommerce.CatalogService.API.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;
using Space.Abstraction;

namespace ECommerce.CatalogService.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class ProductController(ISpace space) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProductsByCategoryId([FromQuery] GetProductsByCategoryIdQuery request)
    {
        var response = await space.Send(request);
        return Ok(response);
    }
}
