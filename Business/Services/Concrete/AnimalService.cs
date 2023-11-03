namespace Business.Services.Concrete
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public AnimalService(IMapper mapper, IAnimalRepository animalRepository, IWebHostEnvironment env, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _animalRepository = animalRepository;
            _env = env;
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(AnimalPostDTO postDto)
        {
            Animal animal = _mapper.Map<Animal>(postDto);
            animal.ImageName = FileExtention.CreateFile(postDto.File, _env.WebRootPath, "assets/img");
           
            if (postDto.SelectedCategories != null)
            {
                var selectedCategories = await _categoryRepository.GetEntityByIdsAsync(postDto.SelectedCategories);

                foreach (var item in selectedCategories)
                {
                    animal.Categories.Add(item);
                }
            }

            await _animalRepository.CreateAsync(animal);
            await _animalRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Animal animal = await _animalRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (animal == null)
            {
                throw new NotFoundException(Messages.AnimalNotFound);
            }

            animal.IsDeleted = true;

            _animalRepository.Update(animal);
            await _animalRepository.SaveAsync();
        }

        public async Task<List<AnimalGetDTO>> GetAllAsync()
        {
            List<Animal> animals = await _animalRepository.GetAllAsync(b => !b.IsDeleted);
            if (animals.Count == 0)
            {
                throw new NotFoundException(Messages.AnimalNotFound);
            }
            return _mapper.Map<List<AnimalGetDTO>>(animals);
        }

        public async Task<List<AnimalGetDTO>> GetAllIncludingAsync()
        {
            Expression<Func<Animal, object>>[] includes = { s => s.Categories };
            List<Animal> animals = _animalRepository.GetAllIncluding(includes).ToList();

            if (!animals.Any())
            {
                throw new NotFoundException(Messages.AnimalNotFound);
            }

            return _mapper.Map<List<AnimalGetDTO>>(animals);
        }
        public async Task<AnimalGetDTO> GetByIdAsync(int id)
        {
            Animal animal = await _animalRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (animal == null)
            {
                throw new NotFoundException(Messages.AnimalNotFound);
            }
            return _mapper.Map<AnimalGetDTO>(animal);
        }

        public async Task<AnimalGetDTO> GetByIdIncludingAsync(int id)
        {
            Expression<Func<Animal, object>>[] includes = { c => c.Categories };
            Animal animal = await _animalRepository.GetIncludingAsync(c => c.Id == id, includes);

            if (animal == null)
            {
                throw new NotFoundException(Messages.AnimalNotFound);
            }

            return _mapper.Map<AnimalGetDTO>(animal);
        }

        public async Task<AnimalGetDTO> GetAllIncluding(int id)
        {
            Animal animal = await _animalRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (animal == null)
            {
                throw new NotFoundException(Messages.AnimalNotFound);
            }
            return _mapper.Map<AnimalGetDTO>(animal);
        }

        public async Task UpdateAsync(AnimalUpdateDTO updateDto)
        {
            Animal animal = await _animalRepository.GetAsync(b => b.Id == updateDto.animalGetDTO.Id && !b.IsDeleted);

            if (animal == null)
            {
                throw new NotFoundException(Messages.AnimalNotFound);
            }

            animal.Name = updateDto.animalPostDTO.Name;
            animal.Categories.Clear();

            if (updateDto.animalPostDTO.SelectedCategories != null)
            {
                var selectedCategories = await _categoryRepository.GetEntityByIdsAsync(updateDto.animalPostDTO.SelectedCategories);
                animal.Categories.AddRange(selectedCategories);
            }

            if (updateDto.animalPostDTO.File != null)
            {
                animal.ImageName = FileExtention.CreateFile(updateDto.animalPostDTO.File, _env.WebRootPath, "assets/img");
            }

            _animalRepository.Update(animal);
            await _animalRepository.SaveAsync();
        }
    }
}
