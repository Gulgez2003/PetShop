using Core.Entities.DataAccess.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class CategoryRepository : EntityRepositoryBase<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
