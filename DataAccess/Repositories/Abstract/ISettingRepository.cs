using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface ISettingRepository : IEntityRepository<Setting, AppDbContext>
    {
    }
}
