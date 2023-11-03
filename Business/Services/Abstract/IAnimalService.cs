namespace Business.Services.Abstract
{
    public interface IAnimalService
    {
        Task<List<AnimalGetDTO>> GetAllAsync();
        Task<List<AnimalGetDTO>> GetAllIncludingAsync();
        Task<AnimalGetDTO> GetByIdIncludingAsync(int id);
        Task<AnimalGetDTO> GetByIdAsync(int id);
        Task CreateAsync(AnimalPostDTO postDto);
        Task UpdateAsync(AnimalUpdateDTO updateDto);
        Task DeleteAsync(int id);
    }
}
