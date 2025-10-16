namespace ECommerce.CatalogService.API.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public decimal Price { get; set; }

    public Guid InventoryItemId { get; set; } = Guid.CreateVersion7();

    public List<ProductImage> Images { get; set; } = new();

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }

    public List<ProductSpecification> Specifications { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}

