namespace Business.Services.Abstract
{
    public interface IOrderService
    {
        Task<List<OrderGetDTO>> GetAllAsync();
        Task<OrderGetDTO> GetByIdAsync(int id);
        Task CreateAsync(OrderPostDTO postDto);
    }
}
