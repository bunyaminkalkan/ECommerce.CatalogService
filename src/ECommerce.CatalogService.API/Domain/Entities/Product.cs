namespace ECommerce.CatalogService.API.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public decimal Price { get; set; }

    public Guid InventoryItemId { get; set; } // InventoryService’deki item id

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

