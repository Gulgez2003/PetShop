namespace PetShop.app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;
        public ProductController(IProductService productService, ISubCategoryService subCategoryService)
        {
            _productService = productService;
            _subCategoryService = subCategoryService;
        }
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            List<ProductGetDTO> products = await _productService.GetAllIncludingAsync();
            return View(products);
        }

        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ProductGetDTO product=await _productService.GetByIdIncludingAsync(id);
            if (product == null || product.IsDeleted)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.SubCategories = await _subCategoryService.GetAllAsync();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductPostDTO postDto)
        {
            ProductPostDTOValidator validations = new ProductPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDto);
            if (validationResult.IsValid)
            {
                await _productService.CreateAsync(postDto);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View();
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.SubCategories = await _subCategoryService.GetAllAsync();

            ProductUpdateDTO productUpdateDTO = new ProductUpdateDTO
            {
                productGetDTO = await _productService.GetByIdAsync(id)
            };

            return View(productUpdateDTO);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductUpdateDTO updateDto)
        {
            ProductPostDTOValidator validations = new ProductPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDto.productPostDTO);
            if (validationResult.IsValid)
            {
                await _productService.UpdateAsync(updateDto);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View();
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteAsync(id);

            return Json(new { status = 200 });
        }
    }
}

