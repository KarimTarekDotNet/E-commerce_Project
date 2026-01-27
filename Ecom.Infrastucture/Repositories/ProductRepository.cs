using AutoMapper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastucture.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null)
                return false;
            var product = mapper.Map<Product>(productDTO);

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var imagePath = await imageManagementService.AddImageAsync(productDTO.Photo, product.Name);
            var photo = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO productDTO)
        {
            if (productDTO == null)
                return false;

            var product = await context.Products
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == productDTO.Id);

            if (product == null)
                return false;

            mapper.Map(productDTO, product);

            if (productDTO.Photo != null && productDTO.Photo.Count > 0)
            {
                foreach (var photo in product.Photos)
                {
                    imageManagementService.RemoveImageAsync(photo.ImageName);
                }

                context.Photos.RemoveRange(product.Photos);

                var imagePaths = await imageManagementService.AddImageAsync(productDTO.Photo, productDTO.Name);

                var newPhotos = imagePaths.Select(path => new Photo
                {
                    ImageName = path,
                    ProductId = product.Id
                });

                await context.Photos.AddRangeAsync(newPhotos);
            }

            await context.SaveChangesAsync();
            return true;
        }
        public async Task DeleteAsync(Product product)
        {
            var photo = await context.Photos.Where(m => m.ProductId == product.Id)
            .ToListAsync();
            foreach (var item in photo)
            {
                imageManagementService.RemoveImageAsync(item.ImageName);
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}