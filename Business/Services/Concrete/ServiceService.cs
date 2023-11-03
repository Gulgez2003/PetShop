namespace Business.Services.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IMapper mapper, IServiceRepository serviceRepository)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
        }

        public async Task CreateAsync(ServicePostDTO postDto)
        {
            Service service = _mapper.Map<Service>(postDto);

            await _serviceRepository.CreateAsync(service);
            await _serviceRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Service service = await _serviceRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (service == null)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            service.IsDeleted = true;

            _serviceRepository.Update(service);
            await _serviceRepository.SaveAsync();
        }

        public async Task<List<ServiceGetDTO>> GetAllAsync()
        {
            List<Service> services = await _serviceRepository.GetAllAsync(b => !b.IsDeleted);
            if (services.Count == 0)
            {
                throw new NotFoundException(Messages.ServiceNotFound);
            }
            return _mapper.Map<List<ServiceGetDTO>>(services);
        }

        public async Task<ServiceGetDTO> GetByIdIncludingAsync(int id)
        {
            Service service = await _serviceRepository.GetIncludingAsync(c => c.Id == id);

            if (service == null)
            {
                throw new NotFoundException(Messages.ServiceNotFound);
            }

            return _mapper.Map<ServiceGetDTO>(service);
        }

        public async Task<ServiceGetDTO> GetByIdAsync(int id)
        {
            Service service = await _serviceRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (service == null)
            {
                throw new NotFoundException(Messages.ServiceNotFound);
            }
            return _mapper.Map<ServiceGetDTO>(service);
        }

        public async Task UpdateAsync(ServiceUpdateDTO updateDto)
        {
            Service service = await _serviceRepository.GetAsync(b => b.Id == updateDto.serviceGetDTO.Id && !b.IsDeleted);
            if (service == null)
            {
                throw new NotFoundException(Messages.ProductNotFound);
            }
            service.Name = updateDto.servicePostDTO.Name;
            service.Description = updateDto.servicePostDTO.Description;
            service.Icon = updateDto.servicePostDTO.Icon;

            _serviceRepository.Update(service);
            await _serviceRepository.SaveAsync();
        }
    }
}
