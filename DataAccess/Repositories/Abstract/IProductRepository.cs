using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository : IEntityRepository<Product, AppDbContext>
    {
    }
}
