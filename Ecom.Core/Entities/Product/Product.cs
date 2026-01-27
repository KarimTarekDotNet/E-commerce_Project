using Ecom.Core.Entities;
using Ecom.Core.Entities.Product;

namespace Ecom.Core.Entities.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}