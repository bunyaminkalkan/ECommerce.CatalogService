using ECommerce.BuildingBlocks.Shared.Kernel.Exceptions;
using ECommerce.CatalogService.API.Data.Context;
using ECommerce.CatalogService.API.Domain.Entities;
using ECommerce.CatalogService.API.UseCases.Commands.Brands;
using Microsoft.EntityFrameworkCore;
using Space.Abstraction;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace ECommerce.CatalogService.API.UseCases.Handlers;

public class AdminBrandHandler(AppDbContext appDbContext)
{
    [Handle]
    public async Task<Nothing> CreateBrandAsync(HandlerContext<CreateBrandCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var brand = new Brand
        {
            Name = ctx.Request.Name,
            LogoUrl = ctx.Request.LogoUrl,
        };

        await appDbContext.Brands.AddAsync(brand);
        await appDbContext.SaveChangesAsync();

        return Nothing.Value;
    }

    [Handle]
    public async Task<Brand> UpdateBrandAsync(HandlerContext<UpdateBrandCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var brand = await appDbContext.Brands.FindAsync(ctx.Request.Id);

        if (brand == null)
            throw new BadRequestException("Brand is not found");

        brand.Name = ctx.Request.Name;
        brand.LogoUrl = ctx.Request.LogoUrl;

        appDbContext.Brands.Update(brand);
        await appDbContext.SaveChangesAsync();

        return brand;
    }

    [Handle]
    public async Task<Nothing> DeleteBrandAsync(HandlerContext<DeleteBrandCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var brand = await appDbContext.Brands.FindAsync(ctx.Request.Id);

        if (brand == null)
            throw new BadRequestException("Brand is not found");

        appDbContext.Brands.Remove(brand);
        await appDbContext.SaveChangesAsync();

        return Nothing.Value;
    }
}
