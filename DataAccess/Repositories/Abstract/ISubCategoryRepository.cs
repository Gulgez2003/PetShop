using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface ISubCategoryRepository : IEntityRepository<SubCategory, AppDbContext>
    {
    }
}
