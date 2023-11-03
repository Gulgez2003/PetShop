using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category, AppDbContext>
    {
    }
}
