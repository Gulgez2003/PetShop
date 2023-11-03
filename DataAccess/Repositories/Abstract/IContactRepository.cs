using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface IContactRepository : IEntityRepository<Contact, AppDbContext>
    {
    }
}
