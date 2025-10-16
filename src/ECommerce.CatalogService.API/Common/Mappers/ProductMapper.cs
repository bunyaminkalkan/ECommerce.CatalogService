using ECommerce.CatalogService.API.Domain.Entities;
using ECommerce.CatalogService.API.UseCases.Responses;

namespace ECommerce.CatalogService.API.Common.Mappers;

public static class ProductMapper
{
    public static ProductResponse MapProductResponse(Product product)
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

    public static IEnumerable<ProductResponse> MapProductResponseList(IEnumerable<Product> products)
    {
        return products.Select(MapProductResponse).ToList();
    }
}
