namespace PetShop.app.Controllers
{
    public class PaymentController : Controller
    {
        public const string COOKIES_BASKET = "basket";
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<BasketVM> basketVMs = GetBasketVMs();
            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
            foreach (var item in basketVMs)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId && !p.IsDeleted)
                    .Include(p => p.SubCategory)
                    .FirstOrDefaultAsync();
                BasketItemVM basketItemVM = new BasketItemVM()
                {
                    Id = item.ProductId,
                    ProductCount = item.Count,
                    Name = product.Name,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    ImageName = product.ImageName,
                    InStock = product.InStock,
                    IsDeleted = product.IsDeleted,
                    SubCategoryName = product.SubCategory.Name
                };
                basketItemVMs.Add(basketItemVM);
            }

            return View(basketItemVMs);
        }

        public IActionResult Confirmation()
        {
            // Здесь вы можете выполнить необходимые действия для подготовки данных для страницы подтверждения.
            // Например, получить информацию о заказе и отобразить ее на странице подтверждения.

            return View();
        }

        private List<BasketVM> GetBasketVMs()
        {
            List<BasketVM> basketVMs;

            if (Request.Cookies[COOKIES_BASKET] != null)
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies[COOKIES_BASKET]);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }

            return basketVMs;
        }

        public PaymentMethod CreatePaymentMethod(string token)
        {
            StripeConfiguration.ApiKey = "sk_test_51Nw79ABTPFngtY7aHNXIBawzVpPppU0jkpzvTmpGerj0qUZgO7NapgFne2TlDSGzfAHHY5TEeRWXAesLVLoJ2YK0004ANKw9HT"; // Замените на ваш секретный ключ Stripe

            var paymentMethodService = new PaymentMethodService();

            var paymentMethodCreateOptions = new PaymentMethodCreateOptions
            {
                Type = "card", // Указываем тип способа оплаты (карта)
                Card = new PaymentMethodCardOptions
                {
                    Token = token // Передаем токен в опции создания карты
                }
            };


            try
            {
                PaymentMethod paymentMethod = paymentMethodService.Create(paymentMethodCreateOptions);

                // Здесь вы можете дополнительно обработать созданный PaymentMethod
                // Например, вы можете сохранить его ID в базе данных для будущего использования

                return paymentMethod;
            }
            catch (StripeException e)
            {
                // Обработка ошибки Stripe
                // Здесь можно добавить логику для обработки ошибки, например, запись в журнал ошибок

                throw; // Возможно, вам нужно выбросить исключение или вернуть сообщение об ошибке
            }
        }

        [HttpPost]
        public IActionResult ProcessPayment(string stripeToken)
        {
            // Ваш код для обработки оплаты через Stripe
            // Используйте ваш Secret API Key Stripe
            StripeConfiguration.ApiKey = "sk_test_51Nw79ABTPFngtY7aHNXIBawzVpPppU0jkpzvTmpGerj0qUZgO7NapgFne2TlDSGzfAHHY5TEeRWXAesLVLoJ2YK0004ANKw9HT";

            // Получите данные о товарах из сессии или куки
            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(basketItemVMs.Sum(item => item.Price * item.ProductCount) * 100), // Сумма платежа в центах
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
                Description = "Оплата заказа",
                Confirm = true,
                CaptureMethod = "automatic",
                PaymentMethod = stripeToken
            };

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(options);

            // Обработайте результат оплаты
            if (paymentIntent.Status == "succeeded")
            {
                // Платеж успешно завершен, можно обновить статус заказа и очистить корзину
                // Редирект на страницу подтверждения
                return RedirectToAction("Confirmation");
            }
            else
            {
                // Платеж не прошел, отобразить ошибку
                ViewBag.ErrorMessage = "Платеж не удался. Пожалуйста, попробуйте еще раз.";
                return View("Index");
            }

            return View("Confirmation"); // Перенаправьте пользователя на страницу подтверждения
        }

    }
}
