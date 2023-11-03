using Entities.Concrete;

namespace PetShop.app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: ServiceController
        public async Task<IActionResult> Index()
        {
            List<ServiceGetDTO> services = await _serviceService.GetAllAsync();
            return View(services);
        }

        public async Task<IActionResult> Details(int id)
        {
            ServiceGetDTO service = await _serviceService.GetByIdIncludingAsync(id);
            if (service == null || service.IsDeleted)
            {
                return NotFound();
            }
            return View(service);
        }

        // GET: ServiceController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServicePostDTO postDto)
        {
            ServicePostDTOValidator validations = new ServicePostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDto);
            if (validationResult.IsValid)
            {
                await _serviceService.CreateAsync(postDto);
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

        // GET: ServiceController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            ServiceUpdateDTO serviceUpdateDTO = new ServiceUpdateDTO
            {
                serviceGetDTO = await _serviceService.GetByIdAsync(id)
            };

            return View(serviceUpdateDTO);
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServiceUpdateDTO updateDto)
        {
            ServicePostDTOValidator validations = new ServicePostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDto.servicePostDTO);
            if (validationResult.IsValid)
            {
                await _serviceService.UpdateAsync(updateDto);
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

        // GET: Admin/Service/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            await _serviceService.DeleteAsync(id);

            return Json(new { status = 200 });
        }
    }
}
