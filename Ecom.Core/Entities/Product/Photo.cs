using Ecom.Core.Entities;

namespace Ecom.Core.Entities.Product
{
    public class Photo : BaseEntity<int>
    {
        public string ImageName { get; set; } = null!;
        public int ProductId { get; set; }

        //public virtual Product Product { get; set; } = null!;
    }
}