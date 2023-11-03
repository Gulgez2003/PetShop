namespace Business.Services.Abstract
{
    public interface IServiceService
    {
        Task<List<ServiceGetDTO>> GetAllAsync();
        Task<ServiceGetDTO> GetByIdIncludingAsync(int id);
        Task<ServiceGetDTO> GetByIdAsync(int id);
        Task CreateAsync(ServicePostDTO postDto);
        Task UpdateAsync(ServiceUpdateDTO updateDto);
        Task DeleteAsync(int id);
    }
}
