namespace Business.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(OrderPostDTO postDto)
        {
            Order order = _mapper.Map<Order>(postDto);

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveAsync();
        }

        public async Task<List<OrderGetDTO>> GetAllAsync()
        {
            List<Order> orders = await _orderRepository.GetAllAsync();
            if (orders.Count == 0)
            {
                throw new NotFoundException(Messages.OrderNotFound);
            }
            return _mapper.Map<List<OrderGetDTO>>(orders);
        }

        public async Task<OrderGetDTO> GetByIdAsync(int id)
        {
            Order order = await _orderRepository.GetAsync(b => b.Id == id);
            if (order == null)
            {
                throw new NotFoundException(Messages.OrderNotFound);
            }
            return _mapper.Map<OrderGetDTO>(order);
        }
    }
}
