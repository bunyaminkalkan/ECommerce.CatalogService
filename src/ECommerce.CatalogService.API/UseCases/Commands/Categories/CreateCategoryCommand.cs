using Space.Abstraction;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Categories;

public record CreateCategoryCommand(
    string Name,
    string? ParentCategoryName
    ) : IRequest<Nothing>;
