using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastucture.Data;

namespace Ecom.Infrastucture.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
