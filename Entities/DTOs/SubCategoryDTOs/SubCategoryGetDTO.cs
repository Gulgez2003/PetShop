namespace Entities.DTOs.SubCategoryDTOs
{
    public class SubCategoryGetDTO
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
        public bool IsDeleted { get; set; }
    }
}
