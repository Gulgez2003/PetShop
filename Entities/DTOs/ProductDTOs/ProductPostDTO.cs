namespace Entities.DTOs.ProductDTOs
{
    public class ProductPostDTO
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile File { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int SubCategoryId { get; set; }
    }
}
