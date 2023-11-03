namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageName { get; set; }
        public bool IsDeleted { get; set; }
        public bool InStock { get; set; }
        public int Count { get; set; }
        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public virtual List<Testimonial> Testimonials { get; set; }

        public Product()
        {
            Testimonials = new List<Testimonial>();
        }
    }
}
