namespace ECommerce.CatalogService.API.Domain.Entities;

public class Category
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }

    public string HierarchyPath { get; set; } = default!;
}
