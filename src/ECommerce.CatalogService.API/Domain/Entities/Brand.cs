namespace ECommerce.CatalogService.API.Domain.Entities;

public class Brand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
}
