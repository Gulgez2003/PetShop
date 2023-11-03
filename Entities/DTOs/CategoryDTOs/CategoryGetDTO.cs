namespace Entities.DTOs.CategoryDTOs
{
    public class CategoryGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Animal> Animals { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
    }
}
