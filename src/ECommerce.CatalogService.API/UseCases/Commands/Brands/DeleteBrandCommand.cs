using Space.Abstraction;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Brands;

public record DeleteBrandCommand(Guid Id) : IRequest<Nothing>;
