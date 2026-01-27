using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastucture.Data;

namespace Ecom.Infrastucture.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}