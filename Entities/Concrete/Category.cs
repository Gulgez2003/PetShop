namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Animal> Animals { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }

        public Category()
        {
            Animals = new List<Animal>();
            SubCategories= new List<SubCategory>();
        }
    }
}
