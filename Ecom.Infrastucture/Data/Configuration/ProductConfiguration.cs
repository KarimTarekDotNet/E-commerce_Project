using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastucture.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Photos)
                   .WithOne(ph => ph.Product)
                   .HasForeignKey(ph => ph.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Product
                {
                    Id = 1,
                    Name = "Sample Product 1",
                    Description = "This is a sample product description.",
                    Price = 19.99m,
                    CategoryId = 1,
                });
        }
    }
}