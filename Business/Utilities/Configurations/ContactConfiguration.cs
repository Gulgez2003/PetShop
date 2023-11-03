namespace Business.Utilities.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(b => b.Name).IsRequired().HasMaxLength(128).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(c => c.Email).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Message).IsRequired();
        }
    }
}
