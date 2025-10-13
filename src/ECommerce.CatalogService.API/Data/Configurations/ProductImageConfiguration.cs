using ECommerce.CatalogService.API.Contants.Tables;
using ECommerce.CatalogService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.CatalogService.API.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable(Tables.ProductImages);

        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(pi => pi.IsMain)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(pi => pi.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(pi => pi.ProductId);

        builder.HasIndex(pi => new { pi.ProductId, pi.IsMain });
    }
}
