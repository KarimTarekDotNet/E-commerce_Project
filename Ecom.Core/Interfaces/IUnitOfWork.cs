namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IProductRepository ProductRepository { get; }
    }
}