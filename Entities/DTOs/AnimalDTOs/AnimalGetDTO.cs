namespace Entities.DTOs.AnimalDTOs
{
    public class AnimalGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageName { get; set; }
        public virtual List<Category> Categories { get; set; }
        public AnimalGetDTO()
        {
            Categories = new List<Category>();
        }
    }
}
