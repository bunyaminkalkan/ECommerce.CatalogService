using ECommerce.CatalogService.API.Contants.Tables;
using ECommerce.CatalogService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.CatalogService.API.Data.Configurations;

public class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        builder.ToTable(Tables.ProductSpecifications);

        builder.HasKey(ps => ps.Id);

        builder.Property(ps => ps.Key)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ps => ps.Value)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(ps => ps.Product)
            .WithMany(p => p.Specifications)
            .HasForeignKey(ps => ps.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ps => ps.ProductId);

        builder.HasIndex(ps => new { ps.ProductId, ps.Key });
    }
}
