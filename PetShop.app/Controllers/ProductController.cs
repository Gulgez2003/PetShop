namespace PetShop.app.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        // GET: ProductController
        public async Task<IActionResult> Index(int currentPage=1, int take=4)
        {
            List<ProductGetDTO> products = await _productService.GetAllPaginatedAsync(currentPage,take);
         
            int pageCount = await GetPageCount(take);
            PaginateVM<ProductGetDTO> paginateVM=new PaginateVM<ProductGetDTO>
            {
                Models=products,
                CurrentPage=currentPage,
                PageCount=pageCount,
                Previous=currentPage>1,
                Next=currentPage<pageCount
            };

            return View(paginateVM);
        }

        public async Task<int> GetPageCount(int take)
        {
            int productCount = await _productService.CountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }

        public async Task<IActionResult> Search(string name)
        {
            List<ProductGetDTO> results = await _productService.GetSearchResults(name);
            List<CategoryGetDTO> categories = await _categoryService.GetAllAsync();
            ProductCategoryTestimonialVM productCategoryTestimonialVM = new ProductCategoryTestimonialVM
            {
                ProductGetDTOs = results,
                CategoryGetDTOs = categories
            };

            return View(productCategoryTestimonialVM);
        }

        public async Task<IActionResult> SearchByCategory(Category category)
        {
            List<ProductGetDTO> results = await _productService.GetSearchResults(category.Name);
            List<CategoryGetDTO> categories = await _categoryService.GetAllAsync();
            ProductCategoryTestimonialVM productCategoryTestimonialVM = new ProductCategoryTestimonialVM
            {
                ProductGetDTOs = results,
                CategoryGetDTOs = categories
            };

            return View(productCategoryTestimonialVM);
        }
    }
}
