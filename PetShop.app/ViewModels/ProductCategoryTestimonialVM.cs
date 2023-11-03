namespace PetShop.app.ViewModels
{
    public class ProductCategoryTestimonialVM
    {
        public ProductGetDTO ProductGetDTO { get; set; }
        public List<ProductGetDTO>? ProductGetDTOs { get; set; }
        public List<CategoryGetDTO>? CategoryGetDTOs { get; set; }
        public TestimonialPostDTO TestimonialPostDTO { get; set; }
        public List<TestimonialGetDTO> TestimonialGetDTOs { get; set; }
    }
}
