using ECommerce.CatalogService.API.UseCases.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Space.Abstraction;
using System;

[ApiController]
[Route("/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminCategoryController(ISpace space) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CrateCategoryAsync([FromBody] CreateCategoryCommand request)
    {
        await space.Send(request);
        return Ok();
    }

    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Guid categoryId, [FromBody] UpdateCategoryCommand request)
    {
        request.Id = categoryId;
        var respone = await space.Send(request);
        return Ok(respone);
    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid categoryId)
    {
        var request = new DeleteCategoryCommand(categoryId);
        await space.Send(request);
        return Ok();
    }
}
