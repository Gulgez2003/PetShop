namespace Business.Utilities.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(s => s.Information).IsRequired().HasMaxLength(256).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Address).IsRequired().HasMaxLength(256);
            builder.Property(s => s.FaceBookIcon).IsRequired();
            builder.Property(s => s.TwitterIcon).IsRequired();
            builder.Property(s => s.InstagramIcon).IsRequired();
        }
    }
}
