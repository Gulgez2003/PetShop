namespace Business.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryGetDTO>> GetAllAsync();
        Task<List<CategoryGetDTO>> GetAllIncludingAsync();
        Task<CategoryGetDTO> GetByIdIncludingAsync(int id);
        Task<CategoryGetDTO> GetByIdAsync(int id);
        Task CreateAsync(CategoryPostDTO postDto);
        Task UpdateAsync(CategoryUpdateDTO updateDto);
        Task DeleteAsync(int id);
    }
}
