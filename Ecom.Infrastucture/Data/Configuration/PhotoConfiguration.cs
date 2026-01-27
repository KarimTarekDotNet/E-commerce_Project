using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastucture.Data.Configuration
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageName).IsRequired().HasMaxLength(100);
            builder.HasData(new Photo { Id = 3, ImageName = "test", ProductId = 1 });
        }
    }
}