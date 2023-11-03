using Core.Entities.DataAccess.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class TestimonialRepository : EntityRepositoryBase<Testimonial, AppDbContext>, ITestimonialRepository
    {
        public TestimonialRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
