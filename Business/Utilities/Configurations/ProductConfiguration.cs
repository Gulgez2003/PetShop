namespace Business.Utilities.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(128).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(p => p.Description).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(128);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.ImageName).IsRequired();
            builder.Property(p => p.Price).IsRequired();
        }
    }
}
