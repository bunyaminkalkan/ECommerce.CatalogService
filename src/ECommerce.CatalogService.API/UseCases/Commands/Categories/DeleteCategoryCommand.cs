using Space.Abstraction;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Categories;

public record DeleteCategoryCommand(Guid Id): IRequest<Nothing>;
