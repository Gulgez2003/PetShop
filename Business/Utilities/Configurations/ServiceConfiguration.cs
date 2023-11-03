namespace Business.Utilities.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(b => b.Name).IsRequired().HasMaxLength(128).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(b => b.Description).IsRequired().HasMaxLength(256);
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.Icon).IsRequired();
        }
    }
}
