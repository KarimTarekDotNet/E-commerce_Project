using Microsoft.AspNetCore.Http;

namespace Ecom.Core.DTOs
{
    public record ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public virtual List<PhotoDTO> Photos { get; set; }
        public string CategoryName { get; set; } = null!;
        public double rating { get; set; }

    }
    public record AddProductDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Photo { get; set; } = null!;
    }
    public record UpdateProductDTO : AddProductDTO
    {
        public int Id { get; set; }
    }
}