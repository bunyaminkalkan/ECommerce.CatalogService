using ECommerce.CatalogService.API.UseCases.Commands.Brands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using Space.Abstraction;

namespace ECommerce.CatalogService.API.Controllers;

[ApiController]
[Route("/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminBrandController(ISpace space) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBrandAsync([FromBody] CreateBrandCommand request)
    {
        var response = await space.Send(request);
        return Ok(response);
    }

    [HttpPut("{brandId:guid}")]
    public async Task<IActionResult> UpdateBrandAsync([FromRoute] Guid brandId, [FromBody] UpdateBrandCommand request)
    {
        request.Id = brandId;
        var response = await space.Send(request);
        return Ok(response);
    }

    [HttpDelete("{brandId:guid}")]
    public async Task<IActionResult> DeleteBrandAsync([FromRoute] Guid brandId)
    {
        var request = new DeleteBrandCommand(brandId);
        var response = await space.Send(request);
        return Ok(response);
    }
}
