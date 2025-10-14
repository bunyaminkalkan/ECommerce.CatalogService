namespace ECommerce.CatalogService.API.UseCases.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;

    public decimal Price { get; set; }

    public Guid InventoryItemId { get; set; }

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
    public string CategorySlug { get; set; } = default!;

    public Guid BrandId { get; set; }
    public string BrandName { get; set; } = default!;

    public List<ProductImageResponse> Images { get; set; } = new();

    public List<ProductSpecificationResponse> Specifications { get; set; } = new();
}

public class ProductImageResponse
{
    public Guid Id { get; set; }
    public string Url { get; set; } = default!;
    public bool IsMain { get; set; }
}

public class ProductSpecificationResponse
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
