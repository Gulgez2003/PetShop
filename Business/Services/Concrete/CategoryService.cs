using Entities.Concrete;

namespace Business.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IAnimalRepository animalRepository, ISubCategoryRepository subCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task CreateAsync(CategoryPostDTO postDto)
        {
            Category category = _mapper.Map<Category>(postDto);

            if (postDto.SelectedAnimals != null)
            {
                var selectedAnimals = await _animalRepository.GetEntityByIdsAsync(postDto.SelectedAnimals);

                category.Animals.AddRange(selectedAnimals);
            }

            if (postDto.SelectedSubCategories != null)
            {
                var selectedSubCategories = await _subCategoryRepository.GetEntityByIdsAsync(postDto.SelectedSubCategories);

                category.SubCategories.AddRange(selectedSubCategories);
            }

            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _categoryRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (category == null)
            {
                throw new NotFoundException(Messages.CategoryNotFound);
            }
            category.IsDeleted = true;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task<List<CategoryGetDTO>> GetAllAsync()
        {
            List<Category> categories = await _categoryRepository.GetAllAsync(b => !b.IsDeleted);
            if (categories.Count == 0)
            {
                throw new NotFoundException(Messages.CategoryNotFound);
            }
            return _mapper.Map<List<CategoryGetDTO>>(categories);
        }

        public async Task<List<CategoryGetDTO>> GetAllIncludingAsync()
        {
            Expression<Func<Category, object>>[] includes = { s => s.Animals, s => s.SubCategories };
            List<Category> categories = _categoryRepository.GetAllIncluding(includes).ToList();

            if (!categories.Any())
            {
                throw new NotFoundException(Messages.CategoryNotFound);
            }

            return _mapper.Map<List<CategoryGetDTO>>(categories);
        }

        public async Task<CategoryGetDTO> GetByIdIncludingAsync(int id)
        {
            Expression<Func<Category, object>>[] includes = { c => c.Animals, c => c.SubCategories };
            Category category = await _categoryRepository.GetIncludingAsync(c => c.Id == id, includes);

            if (category == null)
            {
                throw new NotFoundException(Messages.CategoryNotFound);
            }

            return _mapper.Map<CategoryGetDTO>(category);
        }

        public async Task<CategoryGetDTO> GetByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (category == null)
            {
                throw new NotFoundException(Messages.CategoryNotFound);
            }
            return _mapper.Map<CategoryGetDTO>(category);
        }

        public async Task UpdateAsync(CategoryUpdateDTO updateDto)
        {
            Category category = await _categoryRepository.GetAsync(b => b.Id == updateDto.categoryGetDTO.Id && !b.IsDeleted);
            if (category == null)
            {
                throw new NotFoundException(Messages.CategoryNotFound);
            }

            category.Name = updateDto.categoryPostDTO.Name;

            category.Animals.Clear();
            category.SubCategories.Clear();

            if (updateDto.categoryPostDTO.SelectedAnimals != null)
            {
                var selectedAnimals = await _animalRepository.GetEntityByIdsAsync(updateDto.categoryPostDTO.SelectedAnimals);
                category.Animals.AddRange(selectedAnimals);
            }

            if (updateDto.categoryPostDTO.SelectedSubCategories != null)
            {
                var selectedSubCategories = await _subCategoryRepository.GetEntityByIdsAsync(updateDto.categoryPostDTO.SelectedSubCategories);

                category.SubCategories.AddRange(selectedSubCategories);
            }

            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
        }
    }
}
