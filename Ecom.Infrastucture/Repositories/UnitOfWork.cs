using Ecom.Core.Interfaces;
using Ecom.Infrastucture.Data;

namespace Ecom.Infrastucture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context);
            PhotoRepository = new PhotoRepository(context);
        }

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public IProductRepository ProductRepository { get; }
    }
}