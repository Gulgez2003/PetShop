namespace Entities.DTOs.AnimalDTOs
{
    public class AnimalPostDTO
    {
        public string? Name { get; set; }
        public IFormFile? File { get; set; }
        public int[] SelectedCategories { get; set; }
        public virtual List<Category> Categories { get; set; }

        public AnimalPostDTO()
        {
            Categories = new List<Category>();
        }
    }
}
 