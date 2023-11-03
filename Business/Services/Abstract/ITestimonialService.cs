namespace Business.Services.Abstract
{
    public interface ITestimonialService
    {
        Task<List<TestimonialGetDTO>> GetAllAsync();
        Task<List<TestimonialGetDTO>> GetAllPublishedAsync();
        Task<List<TestimonialGetDTO>> GetAllRejectedAsync();
        Task<List<TestimonialGetDTO>> GetAllIncludingAsync();
        Task<List<TestimonialGetDTO>> GetPublishedByIdAsync(int id);
        Task<TestimonialGetDTO> GetByIdAsync(int id);
        Task CreateAsync(TestimonialPostDTO postDto);
        Task UpdateAsync(TestimonialUpdateDTO updateDto);
        Task DeleteAsync(int id);
        Task ApproveAsync(int id);
        Task DisapproveAsync(int id);
    }
}
