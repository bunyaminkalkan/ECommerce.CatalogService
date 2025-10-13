using ECommerce.CatalogService.API.Contants.Tables;
using ECommerce.CatalogService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.CatalogService.API.Data.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable(Tables.Brand);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.LogoUrl)
            .HasMaxLength(500);

        builder.HasIndex(b => b.Name)
            .IsUnique();
    }
}