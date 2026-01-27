using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastucture.Data;

namespace Ecom.Infrastucture.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
