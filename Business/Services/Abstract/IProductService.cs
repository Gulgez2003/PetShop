namespace Business.Services.Abstract
{
    public interface IProductService
    {
        Task<List<ProductGetDTO>> GetAllAsync();
        Task<List<ProductGetDTO>> GetAllIncludingAsync();
        Task<List<ProductGetDTO>> GetAllPaginatedAsync(int currentPage, int take);
        Task<List<ProductGetDTO>> GetSearchResults(string name);
        Task<List<ProductGetDTO>> GetSearchResultsByCategory(Category category);
        Task<ProductGetDTO> GetByIdIncludingAsync(int id);
        Task<ProductGetDTO> GetByIdAsync(int id);
        Task CreateAsync(ProductPostDTO postDto);
        Task UpdateAsync(ProductUpdateDTO updateDto);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
