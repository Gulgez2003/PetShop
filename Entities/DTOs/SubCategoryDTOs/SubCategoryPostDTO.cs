namespace Entities.DTOs.SubCategoryDTOs
{
    public class SubCategoryPostDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int[] SelectedProducts { get; set; }
        public virtual List<Product> Products { get; set; }
        public SubCategoryPostDTO()
        {
            Products = new List<Product>();
        }
    }
}
