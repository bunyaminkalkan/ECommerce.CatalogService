using ECommerce.CatalogService.API.UseCases.Responses;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Products;

public record CreateProductCommand(
    string Name,
    string Description,
    string ShortDescription,
    decimal Price,
    List<string> Images,
    string CategoryName,
    string BrandName,
    IDictionary<string, string> Specifications
    ) : IRequest<ProductResponse>;
