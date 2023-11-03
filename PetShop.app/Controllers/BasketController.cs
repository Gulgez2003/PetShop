namespace PetShop.app.Controllers
{
    public class BasketController : Controller
    {
        public const string COOKIES_BASKET = "basket";
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
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

        public async Task<IActionResult> AddToBasket(int productId)
        {
            if (productId == 0)
            {
                return NotFound();
            }
            var dbProduct = await _context.Products.Where(p => p.Id == productId && !p.IsDeleted).FirstOrDefaultAsync();
            if (dbProduct == null)
            {
                return BadRequest();
            }

            List<BasketVM> basketVMs = GetBasketVMs();
            CheckBasketVM(productId, basketVMs);

            UpdateCookie(basketVMs);

            return RedirectToAction("Index", "Product");
        }

        private void UpdateCookie(List<BasketVM> basketVMs)
        {
            string cookiesBasket = JsonConvert.SerializeObject(basketVMs);
            Response.Cookies.Append(COOKIES_BASKET, cookiesBasket);
        }

        private void CheckBasketVM(int id, List<BasketVM> basketVMs)
        {
            BasketVM existBasketItem = basketVMs.FirstOrDefault(b => b.ProductId == id);
            if (existBasketItem != null)
            {
                existBasketItem.Count++;
            }
            else
            {
                basketVMs.Add(new BasketVM { ProductId = id, Count = 1 });
            }
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

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            List<BasketVM> basketVMs = GetBasketVMs();
            var itemToRemove = basketVMs.FirstOrDefault(b => b.ProductId == id);

            if (itemToRemove != null)
            {
                basketVMs.Remove(itemToRemove);
                UpdateCookie(basketVMs);
            }

            return Json(new { status = 200 });
        }

    }
}