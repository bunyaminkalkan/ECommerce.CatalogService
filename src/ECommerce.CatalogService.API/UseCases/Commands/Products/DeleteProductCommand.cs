using Space.Abstraction;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Products;

public record DeleteProductCommand(Guid Id) : IRequest<Nothing>;
