using ECommerce.CatalogService.API.UseCases.Responses;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Products;

public class UpdateProductCommand : IRequest<ProductResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public decimal Price { get; set; }
    public List<string> Images { get; set; }
    public string CategoryName { get; set; }
    public string BrandName { get; set; }
    public IDictionary<string, string> Specifications { get; set; }
}
