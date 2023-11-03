using Entities.Concrete;

namespace PetShop.app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        public SubCategoryController(ISubCategoryService subCategoryService, IProductService productService, ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: SubCategoryController
        public async Task<IActionResult> Index()
        {

            List<SubCategoryGetDTO> subCategories = await _subCategoryService.GetAllIncludingAsync();
            return View(subCategories);
        }

        public async Task<ActionResult> Details(int id)
        {
            SubCategoryGetDTO subCategory = await _subCategoryService.GetByIdIncludingAsync(id);
            if (subCategory == null || subCategory.IsDeleted)
            {
                return NotFound();
            }
            return View(subCategory);
        }

        // GET: SubCategoryController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _productService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        // POST: SubCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubCategoryPostDTO postDto)
        {
            SubCategoryPostDTOValidator validations = new SubCategoryPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDto);
            if (validationResult.IsValid)
            {
                await _subCategoryService.CreateAsync(postDto);
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

        // GET: SubCategoryController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Products = await _productService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();

            SubCategoryUpdateDTO subCategoryUpdateDTO = new SubCategoryUpdateDTO
            {
                subCategoryGetDTO = await _subCategoryService.GetByIdAsync(id)
            };

            return View(subCategoryUpdateDTO);
        }

        // POST: SubCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SubCategoryUpdateDTO updateDto)
        {
            SubCategoryPostDTOValidator validations = new SubCategoryPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDto.subCategoryPostDTO);
            if (validationResult.IsValid)
            {
                await _subCategoryService.UpdateAsync(updateDto);
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

        // GET: Admin/SubCategory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subCategory = await _subCategoryService.GetByIdAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            await _subCategoryService.DeleteAsync(id);

            return Json(new { status = 200 });
        }
    }
}
