using ECommerce.CatalogService.API.Domain.Entities;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Categories;

public class UpdateCategoryCommand : IRequest<Category>
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public string? ParentCategoryName { get; set; }
}
