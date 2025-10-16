using ECommerce.BuildingBlocks.Shared.Kernel.Exceptions;
using ECommerce.CatalogService.API.Common.Mappers;
using ECommerce.CatalogService.API.Data.Context;
using ECommerce.CatalogService.API.Domain.Entities;
using ECommerce.CatalogService.API.UseCases.Commands.Products;
using ECommerce.CatalogService.API.UseCases.Responses;
using Microsoft.EntityFrameworkCore;
using Space.Abstraction;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace ECommerce.CatalogService.API.UseCases.Handlers;

public class AdminProductHandler(AppDbContext appDbContext)
{
    [Handle]
    public async Task<ProductResponse> CreateProductAsync(HandlerContext<CreateProductCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var request = ctx.Request;

        var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Name == request.CategoryName)
            ?? throw new BadRequestException($"{request.CategoryName} not found");

        var brand = await appDbContext.Brands.FirstOrDefaultAsync(b => b.Name == request.BrandName)
            ?? throw new BadRequestException($"{request.BrandName} not found");

        var product = new Product
        {
            Name = request.Name,
            Slug = request.Name.ToLowerInvariant(),
            Description = request.Description,
            ShortDescription = request.ShortDescription,
            Price = request.Price,
            Category = category,
            CategoryId = category.Id,
            Brand = brand,
            BrandId = brand.Id,
        };

        var productImages = request.Images.Select(url => new ProductImage
        {
            Url = url,
            Product = product,
            ProductId = product.Id,
            IsMain = false,
        }).ToList();

        var productSpecifications = request.Specifications.Select(s => new ProductSpecification
        {
            Key = s.Key,
            Value = s.Value,
            Product = product,
            ProductId = product.Id,
        }).ToList();

        product.Images = productImages;
        product.Specifications = productSpecifications;

        await appDbContext.Products.AddAsync(product);
        await appDbContext.ProductImages.AddRangeAsync(productImages);
        await appDbContext.ProductSpecification.AddRangeAsync(productSpecifications);
        await appDbContext.SaveChangesAsync();

        return ProductMapper.MapProductResponse(product);
    }

    [Handle]
    public async Task<ProductResponse> UpdateProductAsync(HandlerContext<UpdateProductCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var request = ctx.Request;

        var product = await appDbContext.Products
            .Where(p => p.Id == request.Id)
            .Include(p => p.Images)
            .Include(p => p.Specifications)
            .FirstOrDefaultAsync()
            ?? throw new BadRequestException("Product is not found");

        var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Name == request.CategoryName)
            ?? throw new BadRequestException($"{request.CategoryName} not found");

        var brand = await appDbContext.Brands.FirstOrDefaultAsync(b => b.Name == request.BrandName)
            ?? throw new BadRequestException($"{request.BrandName} not found");

        product.Name = request.Name;
        product.Description = request.Description;
        product.ShortDescription = request.ShortDescription;
        product.Price = request.Price;
        product.Category = category;
        product.CategoryId = category.Id;
        product.Brand = brand;
        product.BrandId = brand.Id;

        //Temporary solved
        appDbContext.ProductImages.RemoveRange(product.Images);
        appDbContext.ProductSpecification.RemoveRange(product.Specifications);

        var productImages = request.Images.Select(url => new ProductImage
        {
            Url = url,
            Product = product,
            ProductId = product.Id,
            IsMain = false,
        }).ToList();

        var productSpecifications = request.Specifications.Select(s => new ProductSpecification
        {
            Key = s.Key,
            Value = s.Value,
            Product = product,
            ProductId = product.Id,
        }).ToList();

        await appDbContext.ProductImages.AddRangeAsync(productImages);
        await appDbContext.ProductSpecification.AddRangeAsync(productSpecifications);
        //

        product.Images = productImages;
        product.Specifications = productSpecifications;

        appDbContext.Products.Update(product);
        await appDbContext.SaveChangesAsync();

        return ProductMapper.MapProductResponse(product);
    }

    [Handle]
    public async Task<Nothing> DeleteProductAsync(HandlerContext<DeleteProductCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var product = await appDbContext.Products
            .Where(p => p.Id == ctx.Request.Id)
            .Include(p => p.Images)
            .Include(p => p.Specifications)
            .FirstOrDefaultAsync()
            ?? throw new BadRequestException("Product is not found");

        appDbContext.Products.Remove(product);
        appDbContext.ProductImages.RemoveRange(product.Images);
        appDbContext.ProductSpecification.RemoveRange(product.Specifications);
        await appDbContext.SaveChangesAsync();

        return Nothing.Value;
    }
}
