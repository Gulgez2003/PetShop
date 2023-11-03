namespace Entities.DTOs.ProductDTOs
{
    public class ProductGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageName { get; set; }
        public bool IsDeleted { get; set; }
        public bool InStock { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public SubCategory SubCategory { get; set; }
        public virtual List<Testimonial> Testimonials { get; set; }
    }
}
