using ECommerce.CatalogService.API.Domain.Entities;
using Space.Abstraction.Contracts;

namespace ECommerce.CatalogService.API.UseCases.Commands.Brands;

public class UpdateBrandCommand : IRequest<Brand>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
}
