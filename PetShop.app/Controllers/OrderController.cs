namespace PetShop.app.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public const string COOKIES_BASKET = "basket";
        private readonly AppDbContext _context;
        public OrderController(IOrderService orderService, AppDbContext context)
        {
            _orderService = orderService;
            _context = context;
        }

        // GET: OrderController/Index
        public async Task<IActionResult> Index()
        {
            List<OrderGetDTO> orders = await _orderService.GetAllAsync();
            return View(orders);
        }

        // GET: OrderController/Create
        public async Task<IActionResult> Create(OrderPostDTO postDTO)
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
            OrderBasketItemVM orderBasketItemVM = new OrderBasketItemVM
            {
                OrderPostDTO = postDTO,
                BasketItemVMs = basketItemVMs
            };

            return View(orderBasketItemVM);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderBasketItemVM orderBasketItemVM)
        {
            OrderPostDTOValidator validations = new OrderPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(orderBasketItemVM.OrderPostDTO);
            if (validationResult.IsValid)
            {
                await _orderService.CreateAsync(orderBasketItemVM.OrderPostDTO);
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

        public async Task<IActionResult> Confirmation()
        {
            return View();
        }
    }
}
