namespace Entities.DTOs.CategoryDTOs
{
    public class CategoryPostDTO
    {
        public string? Name { get; set; }
        public int[] SelectedSubCategories { get; set; }
        public int[] SelectedAnimals { get; set; }
        public virtual List<Animal> Animals { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
        public CategoryPostDTO()
        {
            SubCategories = new List<SubCategory> ();
            Animals = new List<Animal>();
        }
    }
}
