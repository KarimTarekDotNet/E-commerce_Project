namespace Ecom.Core.DTOs
{
    public record CategoryDTO
    (string Name, string Description);
    public record UpdateCategoryDTO
    (int Id, string Name, string Description);
}
