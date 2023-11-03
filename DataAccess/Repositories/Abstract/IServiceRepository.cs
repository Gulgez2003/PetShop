using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface IServiceRepository : IEntityRepository<Service, AppDbContext>
    {
    }
}
