namespace PetShop.app.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        // GET: Admin/Testimonial/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var testimonial = await _testimonialService.GetByIdAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            await _testimonialService.DeleteAsync(id);

            return Json(new { status = 200 });
        }
    }
}
