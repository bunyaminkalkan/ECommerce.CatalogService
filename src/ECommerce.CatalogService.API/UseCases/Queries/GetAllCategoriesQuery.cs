using ECommerce.CatalogService.API.UseCases.Responses;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Queries;

public sealed record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryResponse>>;
