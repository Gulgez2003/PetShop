namespace PetShop.app.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly AppDbContext _context;
        public const string COOKIES_FAVOURITE = "favourite";
        public FavouriteController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<FavouriteVM> favouriteVMs = GetFavouriteVMs();
            List<FavouriteItemVM> favouriteItems = new List<FavouriteItemVM>();
            foreach (var item in favouriteVMs)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId && !p.IsDeleted)
                    .Include(p => p.SubCategory)
                    .FirstOrDefaultAsync();
                FavouriteItemVM favouriteItemVM = new FavouriteItemVM()
                {
                    Id = item.ProductId,
                    Name = product.Name,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    ImageName = product.ImageName,
                    SubCategoryName = product.SubCategory.Name
                };
                favouriteItems.Add(favouriteItemVM);
            }

            return View(favouriteItems);
        }

        public async Task<IActionResult> AddToFavourite(int productId)
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

            List<FavouriteVM> favouriteVMs = GetFavouriteVMs();
            CheckFavouriteVM(productId, favouriteVMs);

            UpdateCookie(favouriteVMs);

            return RedirectToAction("Index", "Product");
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
                favouriteVMs.Add(new FavouriteVM { ProductId = id});
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

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            List<FavouriteVM> favouriteVMs = GetFavouriteVMs();
            var itemToRemove = favouriteVMs.FirstOrDefault(b => b.ProductId == id);

            if (itemToRemove != null)
            {
                favouriteVMs.Remove(itemToRemove);
                UpdateCookie(favouriteVMs);
            }

            return Json(new { status = 200 });
        }
    }
}
