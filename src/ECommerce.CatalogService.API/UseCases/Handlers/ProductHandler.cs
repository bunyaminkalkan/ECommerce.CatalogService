using ECommerce.CatalogService.API.Common.Mappers;
using ECommerce.CatalogService.API.Data.Context;
using ECommerce.CatalogService.API.UseCases.Queries;
using ECommerce.CatalogService.API.UseCases.Responses;
using Microsoft.EntityFrameworkCore;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace ECommerce.CatalogService.API.UseCases.Handlers;

public class ProductHandler(AppDbContext appDbContext)
{
    [Handle]
    public async Task<IEnumerable<ProductResponse>> GetProductsByCategoryIdAsync(HandlerContext<GetProductsByCategoryIdQuery> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var categoryHierarchyPath = (await appDbContext.Categories.FirstAsync(c => c.Id == ctx.Request.CategoryId)).HierarchyPath;

        var products = await appDbContext.Products
            .Include(p => p.Category)
            .Where(p => p.Category.HierarchyPath.StartsWith(categoryHierarchyPath))
            .Include(p => p.Brand)
            .Include(p => p.Specifications)
            .Include(p => p.Images)
            .ToListAsync();

        return ProductMapper.MapProductResponseList(products);
    }
}
