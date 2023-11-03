namespace PetShop.app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        // GET: TestimonialController
        public async Task<IActionResult> Index()
        {
            List<TestimonialGetDTO> testimonials = await _testimonialService.GetAllAsync();
            return View(testimonials);
        }

        // GET: TestimonialController
        public async Task<IActionResult> PublishedComments(int id)
        {
            List<TestimonialGetDTO> testimonials = await _testimonialService.GetAllPublishedAsync();
            return View(testimonials);
        }

        // GET: TestimonialController
        public async Task<IActionResult> RejectedComments()
        {
            List<TestimonialGetDTO> testimonials = await _testimonialService.GetAllRejectedAsync();
            return View(testimonials);
        }

        // GET: Admin/Testimonial/Approve
        public async Task<IActionResult> Approve(int id)
        {
            var testimonial = await _testimonialService.GetByIdAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("zoosfera7@gmail.com", "ZooSfera");
            mail.To.Add(new MailAddress(testimonial.Email));
            mail.Subject = "Your Comment Has Been Published";
            mail.IsBodyHtml = true;
            string body = string.Empty;

            using (StreamReader stream = new StreamReader("wwwroot/assets/HtmlTemplates/approvedComment.html"))
            {
                body = stream.ReadToEnd();
            }

            mail.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("zoosfera7@gmail.com", "gxij xdox biyz bbux");

            smtp.Send(mail);

            await _testimonialService.ApproveAsync(id);

            return Json(new { status = 200, message = "Comment has been published." });
        }

        // GET: Admin/Testimonial/Disapprove
        public async Task<IActionResult> Disapprove(int id)
        {
            var testimonial = await _testimonialService.GetByIdAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("zoosfera7@gmail.com", "ZooSfera");
            mail.To.Add(new MailAddress(testimonial.Email));
            mail.Subject = "Your Comment Has Been Rejected";
            mail.IsBodyHtml = true;
            string body = string.Empty;

            using (StreamReader stream = new StreamReader("wwwroot/assets/HtmlTemplates/disapprovedComment.html"))
            {
                body = stream.ReadToEnd();
            }

            mail.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("zoosfera7@gmail.com", "gxij xdox biyz bbux");

            smtp.Send(mail);

            await _testimonialService.DisapproveAsync(id);

            return Json(new { status = 200, message = "Comment has been rejected." });
        }
    }
}
