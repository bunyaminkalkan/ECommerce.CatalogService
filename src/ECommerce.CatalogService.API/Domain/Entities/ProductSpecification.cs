namespace ECommerce.CatalogService.API.Domain.Entities;

public class ProductSpecification
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}

