using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.SeedData;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(new Product()
        {
            Id = Guid.Parse("a2a7c567-9166-4401-9f84-1ab5c6abfa36"),
            CreatedUser = Guid.Parse("8408db88-09b8-4efe-ad1a-3a24d292f2dd"),
            Name = "Sapiens",
            Price = 135,
            StockQuantity = 27,
            CreatedDate = DateTime.Now,
            Description = "Yuan Noah Harari"
        });
    }
}