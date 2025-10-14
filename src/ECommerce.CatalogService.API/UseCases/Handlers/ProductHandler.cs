using ECommerce.CatalogService.API.Data.Context;
using ECommerce.CatalogService.API.Domain.Entities;
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

        return MapProductResponseList(products);
    }


    private ProductResponse MapProductResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Slug = product.Slug,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            Price = product.Price,

            InventoryItemId = product.InventoryItemId,

            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? string.Empty,
            CategorySlug = product.Category?.Slug ?? string.Empty,

            BrandId = product.BrandId,
            BrandName = product.Brand?.Name ?? string.Empty,

            Images = product.Images.Select(i => new ProductImageResponse
            {
                Id = i.Id,
                Url = i.Url,
                IsMain = i.IsMain
            }).ToList(),

            Specifications = product.Specifications.Select(s => new ProductSpecificationResponse
            {
                Key = s.Key,
                Value = s.Value
            }).ToList(),
        };
    }

    private IEnumerable<ProductResponse> MapProductResponseList(IEnumerable<Product> products)
    {
        return products.Select(MapProductResponse).ToList();
    }
}
