using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Service>? Services { get; set; }
        public DbSet<Setting>? Settings { get; set; }
        public DbSet<Testimonial>? Testimonials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
