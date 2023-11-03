namespace PetShop.app.ViewModels
{
    public class BasketItemVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageName { get; set; }
        public bool IsDeleted { get; set; }
        public bool InStock { get; set; }
        public string SubCategoryName { get; set; } 
        public int ProductCount { get; set; }
    }
}
