using Entities.Concrete;
using System.Numerics;

namespace Business.Services.Concrete
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public SubCategoryService(IMapper mapper, IProductRepository productRepository, ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(SubCategoryPostDTO postDto)
        {
            SubCategory subCategory = _mapper.Map<SubCategory>(postDto);

            if (postDto.SelectedProducts != null)
            {
                var selectedProducts = await _productRepository.GetEntityByIdsAsync(postDto.SelectedProducts);

                subCategory.Products.AddRange(selectedProducts);
            }          

            await _subCategoryRepository.CreateAsync(subCategory);
            await _subCategoryRepository.SaveAsync();
        }
       
        public async Task DeleteAsync(int id)
        {
            SubCategory subCategory = await _subCategoryRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (subCategory == null)
            {
                throw new NotFoundException(Messages.SubCategoryNotFound);
            }
            subCategory.IsDeleted = true;

            _subCategoryRepository.Update(subCategory);
            await _subCategoryRepository.SaveAsync();
        }

        public async Task<List<SubCategoryGetDTO>> GetAllAsync()
        {
            List<SubCategory> subCategories = await _subCategoryRepository.GetAllAsync(s => !s.IsDeleted);
            if (subCategories.Count == 0)
            {
                throw new NotFoundException(Messages.SubCategoryNotFound);
            }
            return _mapper.Map<List<SubCategoryGetDTO>>(subCategories);
        }

        public async Task<SubCategoryGetDTO> GetByIdAsync(int id)
        {
            SubCategory subCategory = await _subCategoryRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (subCategory == null)
            {
                throw new NotFoundException(Messages.SubCategoryNotFound);
            }
            return _mapper.Map<SubCategoryGetDTO>(subCategory);
        }

        public async Task<List<SubCategoryGetDTO>> GetAllIncludingAsync()
        {
            Expression<Func<SubCategory, object>>[] includes = { s => s.Category };
            List<SubCategory> subCategories = _subCategoryRepository.GetAllIncluding(includes).ToList();

            if (!subCategories.Any())
            {
                throw new NotFoundException(Messages.SubCategoryNotFound);
            }

            return _mapper.Map<List<SubCategoryGetDTO>>(subCategories);
        }

        public async Task<SubCategoryGetDTO> GetByIdIncludingAsync(int id)
        {
            Expression<Func<SubCategory, object>>[] includes = { c => c.Category, c => c.Products };
            SubCategory subCategory = await _subCategoryRepository.GetIncludingAsync(s => s.Id == id, includes);

            if (subCategory == null)
            {
                throw new NotFoundException(Messages.SubCategoryNotFound);
            }

            return _mapper.Map<SubCategoryGetDTO>(subCategory);
        }

        public async Task UpdateAsync(SubCategoryUpdateDTO updateDto)
        {
            SubCategory subCategory = await _subCategoryRepository.GetAsync(b => b.Id == updateDto.subCategoryGetDTO.Id && !b.IsDeleted);

            if (subCategory == null)
            {
                throw new NotFoundException(Messages.SubCategoryNotFound);
            }

            subCategory.Name = updateDto.subCategoryPostDTO.Name;
            subCategory.CategoryId = updateDto.subCategoryPostDTO.CategoryId;
            subCategory.Products.Clear();

            if (updateDto.subCategoryPostDTO.SelectedProducts != null)
            {
                var selectedProducts = await _productRepository.GetEntityByIdsAsync(updateDto.subCategoryPostDTO.SelectedProducts);
                subCategory.Products.AddRange(selectedProducts);
            }


            _subCategoryRepository.Update(subCategory);
            await _subCategoryRepository.SaveAsync();
        }
    }
}
