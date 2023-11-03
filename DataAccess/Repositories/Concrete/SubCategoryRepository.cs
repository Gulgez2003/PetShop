using Core.Entities.DataAccess.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class SubCategoryRepository : EntityRepositoryBase<SubCategory, AppDbContext>, ISubCategoryRepository
    {
        public SubCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
