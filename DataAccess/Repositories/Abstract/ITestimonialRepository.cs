using Core.Entities.DataAccess.Abstract;

namespace DataAccess.Repositories.Abstract
{
    public interface ITestimonialRepository : IEntityRepository<Testimonial, AppDbContext>
    {
    }
}
