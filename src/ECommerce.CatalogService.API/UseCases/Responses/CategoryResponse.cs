namespace ECommerce.CatalogService.API.UseCases.Responses;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
