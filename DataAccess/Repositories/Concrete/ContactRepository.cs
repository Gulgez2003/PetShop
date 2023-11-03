using Core.Entities.DataAccess.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class ContactRepository : EntityRepositoryBase<Contact, AppDbContext>, IContactRepository
    {
        public ContactRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
