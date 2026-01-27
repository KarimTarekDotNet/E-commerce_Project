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
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.NewPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasData(new Product
                {
                    Id = 1,
                    Name = "Sample Product 1",
                    Description = "This is a sample product description.",
                    OldPrice = 19.99m,
                    NewPrice = 15.50m,
                    CategoryId = 1
                });
        }
    }
}