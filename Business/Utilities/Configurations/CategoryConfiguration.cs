namespace Business.Utilities.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(b => b.Name).IsRequired().HasMaxLength(128).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
        }
    }
}
