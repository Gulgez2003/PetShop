using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface IOrderRepository : IEntityRepository<Order, AppDbContext>
    {
    }
}
