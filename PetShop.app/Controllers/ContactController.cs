    namespace PetShop.app.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ISettingService _settingService;
        public ContactController(IContactService contactService, ISettingService settingsService)
        {
            _contactService = contactService;
            _settingService = settingsService;
        }

        // GET: ContactController
        public async Task<IActionResult> Index(ContactPostDTO postDTO)
        {
            SettingGetDTO settings = _settingService.GetSetting();
            SettingContactViewModel settingCommentViewModel = new SettingContactViewModel
            {
                SettingGetDTO = settings,
                ContactPostDTO = postDTO
            };

            return View(settingCommentViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SettingContactViewModel settingCommentViewModel)
        {
            ContactPostDTOValidator validations = new ContactPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(settingCommentViewModel.ContactPostDTO);
            if (validationResult.IsValid)
            {
                await _contactService.CreateAsync(settingCommentViewModel.ContactPostDTO);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(settingCommentViewModel.ContactPostDTO.Email);
                mail.To.Add(new MailAddress("zoosfera7@gmail.com"));
                mail.Subject = "Contact Form";
                mail.IsBodyHtml = true;
                string body = $"<p>FullName:  {settingCommentViewModel.ContactPostDTO.Name} </p>" +
                    $"<p>Email:  {settingCommentViewModel.ContactPostDTO.Email} </p>" +
                    $"<p>Message:  {settingCommentViewModel.ContactPostDTO.Message} </p>";

                mail.Body = body;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("zoosfera7@gmail.com", "gxij xdox biyz bbux");

                smtp.Send(mail);
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
