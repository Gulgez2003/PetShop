namespace PetShop.app.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public const string COOKIES_BASKET = "basket";
        public const string COOKIES_FAVOURITE = "favourite";
        private readonly AppDbContext _context;

        public HomeController(IProductService productService, AppDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        //Add to basket --start--
        public async Task<IActionResult> Index()
        {
            List<ProductGetDTO> products = await _productService.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> AddToBasket(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var dbProduct = await _context.Products.Where(p => p.Id == id && !p.IsDeleted).FirstOrDefaultAsync();
            if (dbProduct == null)
            {
                return BadRequest();
            }

            List<BasketVM> basketVMs = GetBasketVMs();
            CheckBasketVM(id, basketVMs);

            UpdateCookie(basketVMs);

            return RedirectToAction("Index", "Home");
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

        //Add to basket --end--

        //Add to favourite --start--
        public async Task<IActionResult> AddToFavourite(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var dbProduct = await _context.Products.Where(p => p.Id == id && !p.IsDeleted).FirstOrDefaultAsync();
            if (dbProduct == null)
            {
                return BadRequest();
            }

            List<FavouriteVM> favouriteVMs = GetFavouriteVMs();
            CheckFavouriteVM(id, favouriteVMs);

            UpdateCookie(favouriteVMs);

            return RedirectToAction("Index", "Home");
        }

        private void UpdateCookie(List<FavouriteVM> favouriteVMs)
        {
            string cookiesFavourite = JsonConvert.SerializeObject(favouriteVMs);
            Response.Cookies.Append(COOKIES_FAVOURITE, cookiesFavourite);
        }

        private void CheckFavouriteVM(int id, List<FavouriteVM> favouriteVMs)
        {
            FavouriteVM existFavouriteItem = favouriteVMs.FirstOrDefault(b => b.ProductId == id);
            if (existFavouriteItem != null)
            {
                favouriteVMs.Add(existFavouriteItem);
            }
            else
            {
                favouriteVMs.Add(new FavouriteVM { ProductId = id });
            }
        }

        private List<FavouriteVM> GetFavouriteVMs()
        {
            List<FavouriteVM> favouriteVMs;

            if (Request.Cookies[COOKIES_FAVOURITE] != null)
            {
                favouriteVMs = JsonConvert.DeserializeObject<List<FavouriteVM>>(Request.Cookies[COOKIES_FAVOURITE]);
            }
            else
            {
                favouriteVMs = new List<FavouriteVM>();
            }

            return favouriteVMs;
        }

        //Add to favourite --end--
    }
}