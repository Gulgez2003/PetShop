namespace PetShop.app.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ITestimonialService _testimonialService;
        private readonly UserManager<User> _userManager;

        public ProductDetailController(IProductService productService, ICategoryService categoryService, ITestimonialService testimonialService, UserManager<User> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _testimonialService = testimonialService;
            _userManager = userManager;
        }

        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ProductGetDTO product = await _productService.GetByIdIncludingAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            List<CategoryGetDTO> categories = await _categoryService.GetAllAsync();
            List<TestimonialGetDTO> testimonials = await _testimonialService.GetPublishedByIdAsync(id);

            ProductCategoryTestimonialVM productCategoryTestimonialVM = new ProductCategoryTestimonialVM
            {
                ProductGetDTO = product,
                CategoryGetDTOs = categories,
                TestimonialPostDTO = new TestimonialPostDTO(),
                TestimonialGetDTOs = testimonials
            };

            if (product == null)
            {
                return NotFound();
            }

            return View(productCategoryTestimonialVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ProductCategoryTestimonialVM productCategoryTestimonialVM)
        {
            TestimonialPostDTOValidator validations = new TestimonialPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(productCategoryTestimonialVM.TestimonialPostDTO);
            if (validationResult.IsValid)
            {
               
                if (!User.Identity.IsAuthenticated)
                {
                    return Json("Please login or register first");
                }

                string username = User.Identity.Name;
                User user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return Json("Make sure you fill in the fields correctly");
                }

                productCategoryTestimonialVM.TestimonialPostDTO.Email = user.Email;
                productCategoryTestimonialVM.TestimonialPostDTO.FullName = user.FullName;

                await _testimonialService.CreateAsync(productCategoryTestimonialVM.TestimonialPostDTO);
                return Json(new { success = true });
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return Json(new { success = false, message = "Some error message" });
            }
        }
    }
}
