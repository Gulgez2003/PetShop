namespace Entities.Concrete
{
    public class Animal : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageName { get; set; }
        public virtual List<Category> Categories { get; set; }

        public Animal() { 
            Categories = new List<Category>();
        }
    }
}
