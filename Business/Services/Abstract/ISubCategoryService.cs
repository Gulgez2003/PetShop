namespace Business.Services.Abstract
{
    public interface ISubCategoryService
    {
        Task<List<SubCategoryGetDTO>> GetAllAsync();
        Task<List<SubCategoryGetDTO>> GetAllIncludingAsync();
        Task<SubCategoryGetDTO> GetByIdIncludingAsync(int id);
        Task<SubCategoryGetDTO> GetByIdAsync(int id);
        Task CreateAsync(SubCategoryPostDTO postDto);
        Task UpdateAsync(SubCategoryUpdateDTO updateDto);
        Task DeleteAsync(int id);
    }
}
    