using Humanizer;

namespace PetShop.app.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: AccountController/Create
        //public async Task<IActionResult> Create()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    return Json("Ok");
        //}

        // GET: AccountController/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: AccountController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            User user = new User()
            {
                FullName = registerDTO.FullName,
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };
          

            IdentityResult result = new IdentityResult();

            if (registerDTO.Password != null)
            {                
                if(registerDTO.Password == registerDTO.ConfirmPassword)
                {
                    result = await _userManager.CreateAsync(user, registerDTO.Password);
                    
                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The password and confirmation password do not match. Please make sure you enter the same password in both fields.");
                    return View();
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
           

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string? link = Url.Action(nameof(VerifyEmail), "Account", new { email = user.Email, token = token }, Request.Scheme, Request.Host.ToString());

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("zoosfera7@gmail.com", "ZooSfera");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Please verify your email for ZooSfera Petshop";
            mail.IsBodyHtml = true;
            string body = string.Empty;

            using (StreamReader stream = new StreamReader("wwwroot/assets/HtmlTemplates/htmlpage.html"))
            {
                body = stream.ReadToEnd();
            }

            mail.Body = body.Replace("{{link}}", link);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("zoosfera7@gmail.com", "gxij xdox biyz bbux");

            smtp.Send(mail);

            await _userManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound();
            }

            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: AccountController/ForgetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ResetPasswordDto resetPasswordDto)
        {
            User user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user is null)
            {
                return NotFound();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string? link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token = token }, Request.Scheme, Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("zoosfera7@gmail.com", "ZooSfera Petshop");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Please, use the link to reset your password";

            mail.Body = link;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("zoosfera7@gmail.com", "gxij xdox biyz bbux");

            smtp.Send(mail);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound();
            }

            ResetPasswordDto resetPasswordDto = new ResetPasswordDto()
            {
                Email = email,
                Token = token
            };

            return View(resetPasswordDto);
        }

        // POST: AccountController/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {

            User user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user is null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(resetPasswordDto);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                ModelState.AddModelError("", "Email or Password incorrect");
                return View();
            }
            User user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                {
                    ModelState.AddModelError("", "Email or Password incorrect");
                    return View();
                }
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);
            if (!result.Succeeded)
            {
                if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError("", "Please verify your email");
                    return View();
                }
                ModelState.AddModelError("", "Email or Password incorrect");
                return View();
            }
            if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "SuperAdmin"))
            {
                return RedirectToAction("Home", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}
