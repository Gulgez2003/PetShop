namespace Entities.Concrete
{
    public class Testimonial : IEntity
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Comment { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApproved { get; set; }
        public bool IsChecked { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
