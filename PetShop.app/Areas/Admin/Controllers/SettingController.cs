namespace PetShop.app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // GET: SettingController
        public async Task<IActionResult> Index()
        {
            List<SettingGetDTO> settings = await _settingService.GetAllAsync();
            return View(settings);
        }

        // GET: SettingController/Edit
        public async Task<IActionResult> Update(int id)
        {
            SettingUpdateDTO settingUpdateDto = new SettingUpdateDTO
            {
                settingGetDTO = await _settingService.GetByIdAsync(id)
            };
            return View(settingUpdateDto);
        }

        // POST: SettingController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SettingUpdateDTO updateDTO)
        {
            SettingPostDTOValidator validations = new SettingPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDTO.settingPostDTO);
            if (validationResult.IsValid)
            {
                await _settingService.UpdateAsync(updateDTO);
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
    }
}
