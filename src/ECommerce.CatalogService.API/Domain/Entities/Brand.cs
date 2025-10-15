namespace ECommerce.CatalogService.API.Domain.Entities;

public class Brand
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
}
