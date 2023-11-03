namespace Business.Utilities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.FullName).IsRequired().HasMaxLength(128).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(b => b.Email).IsRequired().HasMaxLength(256);
            builder.Property(b => b.UserName).IsRequired().HasMaxLength(256);
            builder.Property(b => b.PasswordHash).IsRequired().HasMaxLength(256);
        }
    }
}
