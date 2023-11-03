using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface IAnimalRepository : IEntityRepository<Animal, AppDbContext>
    {
    }
}
