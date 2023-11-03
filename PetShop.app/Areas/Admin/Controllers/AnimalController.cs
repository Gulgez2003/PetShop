namespace PetShop.app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ICategoryService _categoryService;

        public AnimalController(IAnimalService animalService, ICategoryService categoryService)
        {
            _animalService = animalService;
            _categoryService = categoryService;
        }


        // GET: AnimalController
        public async Task<IActionResult> Index()
        {
            List<AnimalGetDTO> animals = await _animalService.GetAllIncludingAsync();
            return View(animals);
        }

        public async Task<ActionResult> Details(int id)
        {
            AnimalGetDTO animal = await _animalService.GetByIdIncludingAsync(id);
            if (animal == null || animal.IsDeleted)
            {
                return NotFound();
            }
            return View(animal);
        }


        // GET: AnimalController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AnimalPostDTO postDto)
        {
            AnimalPostDTOValidator validations = new AnimalPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDto);
            if (validationResult.IsValid)
            {
                await _animalService.CreateAsync(postDto);
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

        // GET: AnimalController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();

            AnimalUpdateDTO animalUpdateDTO = new AnimalUpdateDTO
            {
                animalGetDTO = await _animalService.GetByIdAsync(id)
            };

            return View(animalUpdateDTO);
        }

        // POST: AnimalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AnimalUpdateDTO updateDto)
        {
            AnimalPostDTOValidator validations = new AnimalPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDto.animalPostDTO);
            if (validationResult.IsValid)
            {
                await _animalService.UpdateAsync(updateDto);
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

        // GET: Admin/Animal/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            await _animalService.DeleteAsync(id);
            return Json(new { status = 200 });
        }
    }
}
