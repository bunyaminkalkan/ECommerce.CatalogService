using ECommerce.CatalogService.API.UseCases.Responses;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Queries;

public record GetProductsByCategoryIdQuery(Guid CategoryId) : IRequest<IEnumerable<ProductResponse>>;
