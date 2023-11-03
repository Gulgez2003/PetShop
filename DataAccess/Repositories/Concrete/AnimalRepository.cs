using Core.Entities.DataAccess.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class AnimalRepository:EntityRepositoryBase<Animal,AppDbContext>, IAnimalRepository
    {
        public AnimalRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
