using ECommerce.BuildingBlocks.Shared.Kernel.Exceptions;
using ECommerce.CatalogService.API.Data.Context;
using ECommerce.CatalogService.API.Domain.Entities;
using ECommerce.CatalogService.API.UseCases.Commands.Categories;
using Microsoft.EntityFrameworkCore;
using Space.Abstraction;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace ECommerce.CatalogService.API.UseCases.Handlers;

public class AdminCategoryHandler(AppDbContext appDbContext)
{
    [Handle]
    public async Task<Nothing> CreateCategoryAsync(HandlerContext<CreateCategoryCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        if (await appDbContext.Categories.AnyAsync(c => c.Name == ctx.Request.Name))
            throw new BadRequestException("Category already exists");

        var parentCategory = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Name == ctx.Request.ParentCategoryName);

        if (parentCategory == null)
            throw new BadRequestException("Parent category is not found");

        var slug = ctx.Request.Name.ToLower();

        var category = new Category
        {
            Name = ctx.Request.Name,
            Slug = slug,
            ParentCategory = parentCategory,
            ParentCategoryId = parentCategory.Id,
            HierarchyPath = parentCategory.HierarchyPath + "/" + slug
        };

        await appDbContext.Categories.AddAsync(category);
        await appDbContext.SaveChangesAsync();

        return Nothing.Value;
    }

    [Handle]
    public async Task<Category> UpdateCategoryAsync(HandlerContext<UpdateCategoryCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == ctx.Request.Id);

        if (category == null)
            throw new BadRequestException("Category is not found");

        bool hierarchyChanged = false;

        var slug = ctx.Request.Name.ToLower();

        if (!string.IsNullOrEmpty(ctx.Request.ParentCategoryName))
        {
            var parentCategory = await appDbContext.Categories
                .FirstOrDefaultAsync(c => c.Name == ctx.Request.ParentCategoryName);

            if (parentCategory == null)
                throw new BadRequestException("Parent category is not found");

            if (category.ParentCategoryId != parentCategory.Id)
            {
                category.ParentCategory = parentCategory;
                category.ParentCategoryId = parentCategory.Id;
                hierarchyChanged = true;
            }
        }
        else if (category.ParentCategoryId != null)
        {
            category.ParentCategory = null;
            category.ParentCategoryId = null;
            hierarchyChanged = true;
        }

        if (category.Name != ctx.Request.Name)
        {
            category.Name = ctx.Request.Name;
            category.Slug = slug;
            hierarchyChanged = true;
        }

        if (hierarchyChanged)
        {
            var oldHierarchy = category.HierarchyPath;

            category.HierarchyPath = category.ParentCategory != null
                ? $"{category.ParentCategory.HierarchyPath}/{slug}"
                : slug;

            await UpdateChildrenHierarchyPaths(category.HierarchyPath, oldHierarchy);
        }

        await appDbContext.SaveChangesAsync();
        return category;
    }

    [Handle]
    public async Task<Nothing> DeleteCategoryAsync(HandlerContext<DeleteCategoryCommand> ctx)
    {
        ctx.CancellationToken.ThrowIfCancellationRequested();

        var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == ctx.Request.Id);

        if (category == null)
            throw new BadRequestException("Category is not found");

        var childCategories = await appDbContext.Categories.Where(c => c.HierarchyPath.Contains(category.Slug)).ToListAsync();

        appDbContext.Categories.Remove(category);
        appDbContext.Categories.RemoveRange(childCategories);
        await appDbContext.SaveChangesAsync();

        return Nothing.Value;
    }


    // Children path update
    private async Task UpdateChildrenHierarchyPaths(string newHierarchyPath, string oldHierarchyPath)
    {
        var childCategories = await appDbContext.Categories.Where(c => c.HierarchyPath.Contains(oldHierarchyPath)).ToListAsync();

        foreach (var child in childCategories)
        {
            child.HierarchyPath = child.HierarchyPath.Replace(oldHierarchyPath, newHierarchyPath);
        }
    }
}
