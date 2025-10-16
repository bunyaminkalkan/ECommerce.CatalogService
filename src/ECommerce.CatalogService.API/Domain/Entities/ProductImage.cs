namespace ECommerce.CatalogService.API.Domain.Entities;

public class ProductImage
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Url { get; set; } = default!;
    public bool IsMain { get; set; } = false;

    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}

