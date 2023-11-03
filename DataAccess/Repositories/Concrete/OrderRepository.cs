using Core.Entities.DataAccess.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class OrderRepository : EntityRepositoryBase<Order, AppDbContext>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
