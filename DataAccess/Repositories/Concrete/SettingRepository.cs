using Core.Entities.DataAccess.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class SettingRepository : EntityRepositoryBase<Setting, AppDbContext>, ISettingRepository
    {
        public SettingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
