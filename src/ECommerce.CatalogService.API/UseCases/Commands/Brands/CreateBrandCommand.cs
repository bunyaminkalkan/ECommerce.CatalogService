using Space.Abstraction;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Brands;

public record CreateBrandCommand(
    string Name,
    string LogoUrl
    ) : IRequest<Nothing>;
