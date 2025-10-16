namespace ECommerce.CatalogService.API.Domain.Entities;

public class ProductSpecification
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}

