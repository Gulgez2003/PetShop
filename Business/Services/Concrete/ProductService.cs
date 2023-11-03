using Entities.Concrete;

namespace Business.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ProductService(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(ProductPostDTO postDto)
        {
            Product product = _mapper.Map<Product>(postDto);
            product.ImageName = FileExtention.CreateFile(postDto.File, _env.WebRootPath, "assets/img");

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _productRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (product == null)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            product.IsDeleted = true;

            _productRepository.Update(product);
            await _productRepository.SaveAsync();
        }

        public async Task<List<ProductGetDTO>> GetAllAsync()
        {
            List<Product> products = await _productRepository.GetAllAsync(b => !b.IsDeleted);
            if (products.Count == 0)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            return _mapper.Map<List<ProductGetDTO>>(products);
        }

        public async Task<List<ProductGetDTO>> GetAllIncludingAsync()
        {
            Expression<Func<Product, object>>[] includes = { p => p.SubCategory, p => p.Testimonials };
            List<Product> products = _productRepository.GetAllIncluding(includes).ToList();

            if (!products.Any())
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }

            return _mapper.Map<List<ProductGetDTO>>(products);
        }

        public async Task<ProductGetDTO> GetByIdAsync(int id)
        {
            Product product = await _productRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (product == null)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            return _mapper.Map<ProductGetDTO>(product);
        }

        public async Task<ProductGetDTO> GetByIdIncludingAsync(int id)
        {
            Expression<Func<Product, object>>[] includes = { p => p.SubCategory, p => p.Testimonials };
            Product product = await _productRepository.GetIncludingAsync(p => p.Id == id, includes);

            if (product == null)
            {
                throw new NotFoundException(Messages.CategoryNotFound);
            }

            return _mapper.Map<ProductGetDTO>(product);
        }

        public async Task<List<ProductGetDTO>> GetAllPaginatedAsync(int currentPage, int take)
        {
            List<Product> products = await _productRepository.GetAllAsync(p => !p.IsDeleted);
            products = products.Skip((currentPage - 1) * take).Take(take).ToList();
;
            if (products.Count == 0)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            return _mapper.Map<List<ProductGetDTO>>(products);
        }

        public async Task<int> CountAsync()
        {
            List<Product> products = await _productRepository.GetAllAsync(b => !b.IsDeleted);
            int count = await _productRepository.CountAsync();
            return count;
        }

        public async Task<List<ProductGetDTO>> GetSearchResults(string name)
        {
            List<Product> products = await _productRepository.GetAllAsync(p => !p.IsDeleted
            && p.Title.Contains(name)
            || p.Name.Contains(name)
            || p.Description.Contains(name));
            if (products == null)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            return _mapper.Map<List<ProductGetDTO>>(products);
        }

        public async Task<List<ProductGetDTO>> GetSearchResultsByCategory(Category category)
        {
            List<Product> products = await _productRepository.GetAllAsync(p => !p.IsDeleted
            && p.Title.Contains(category.Name)
            || p.Name.Contains(category.Name)
            || p.Description.Contains(category.Name));
            if (products == null)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            return _mapper.Map<List<ProductGetDTO>>(products);
        }

        public async Task UpdateAsync(ProductUpdateDTO updateDto)
        {
            Product product = await _productRepository.GetAsync(b => b.Id == updateDto.productGetDTO.Id && !b.IsDeleted);
            if (product == null)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            product.Name = updateDto.productPostDTO.Name;
            product.Title = updateDto.productPostDTO.Title;
            product.Price = updateDto.productPostDTO.Price;
            product.SubCategoryId = updateDto.productPostDTO.SubCategoryId;
            product.Description = updateDto.productPostDTO.Description;

            if (updateDto.productPostDTO.File != null)
            {
                product.ImageName = updateDto.productPostDTO.File.CreateFile(_env.WebRootPath, "assets/img");
            }

            _productRepository.Update(product);
            await _productRepository.SaveAsync();
        }
    }
}
