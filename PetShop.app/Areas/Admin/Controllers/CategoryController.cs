using Entities.Concrete;

namespace PetShop.app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IAnimalService _animalService;
        private readonly ISubCategoryService _subCategoryService;

        public CategoryController(ICategoryService categoryService, IAnimalService animalService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _animalService = animalService;
            _subCategoryService = subCategoryService;
        }

        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            List<CategoryGetDTO> categories = await _categoryService.GetAllIncludingAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            CategoryGetDTO category = await _categoryService.GetByIdIncludingAsync(id);
            if (category == null || category.IsDeleted)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: CategoryController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.SubCategories = await _subCategoryService.GetAllAsync();
            ViewBag.Animals = await _animalService.GetAllAsync();
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryPostDTO postDto)
        {
            CategoryPostDTOValidator validations = new CategoryPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDto);
            if (validationResult.IsValid)
            {
               
                await _categoryService.CreateAsync(postDto);
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

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.SubCategories = await _subCategoryService.GetAllAsync();
            ViewBag.Animals = await _animalService.GetAllAsync();

            CategoryUpdateDTO categoryUpdateDTO = new CategoryUpdateDTO
            {
                categoryGetDTO = await _categoryService.GetByIdAsync(id)
            };

            return View(categoryUpdateDTO);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateDTO updateDto)
        {
            CategoryPostDTOValidator validations = new CategoryPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDto.categoryPostDTO);
            if (validationResult.IsValid)
            {
                await _categoryService.UpdateAsync(updateDto);
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

        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteAsync(id);

            return Json(new { status = 200 });
        }
    }
}
