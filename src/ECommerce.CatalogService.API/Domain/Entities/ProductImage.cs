namespace ECommerce.CatalogService.API.Domain.Entities;

public class ProductImage
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; } = false;

    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}

