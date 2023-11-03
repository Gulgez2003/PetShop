namespace Business.Utilities.Configurations
{
    public class TestimonialConfiguration : IEntityTypeConfiguration<Testimonial>
    {
        public void Configure(EntityTypeBuilder<Testimonial> builder)
        {
            builder.Property(t => t.Comment).IsRequired().HasMaxLength(500).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(t => t.FullName).IsRequired().HasMaxLength(50);
            builder.Property(t => t.IsDeleted).HasDefaultValue(false);
            builder.Property(t => t.IsApproved).HasDefaultValue(false);
            builder.Property(t => t.IsChecked).HasDefaultValue(false);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(50);
        }
    }
}