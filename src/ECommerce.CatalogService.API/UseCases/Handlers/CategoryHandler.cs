using ECommerce.CatalogService.API.Data.Context;
using ECommerce.CatalogService.API.UseCases.Queries;
using ECommerce.CatalogService.API.UseCases.Responses;
using Microsoft.EntityFrameworkCore;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace ECommerce.CatalogService.API.UseCases.Handlers;

public class CategoryHandler(AppDbContext appDbContext)
{
    [Handle]
    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync(HandlerContext<GetAllCategoriesQuery> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var categories = await appDbContext.Categories.ToListAsync();

        return categories.Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            ParentCategoryId = c.ParentCategoryId,
        });
    }
}
