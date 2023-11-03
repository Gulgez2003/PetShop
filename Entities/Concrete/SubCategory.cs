namespace Entities.Concrete
{
    public class SubCategory : IEntity
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Product> Products { get; set; }

        public SubCategory()
        {
            Products = new List<Product>();
        }
    }
}
