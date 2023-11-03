namespace Business.Utilities.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.FirstName).IsRequired().HasMaxLength(128).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(o => o.LastName).IsRequired().HasMaxLength(128);
            builder.Property(o => o.Email).IsRequired().HasMaxLength(256);
            builder.Property(o => o.Phone).IsRequired().HasMaxLength(256);
            builder.Property(o => o.Comment).IsRequired().HasMaxLength(256);
            builder.Property(o => o.Address).IsRequired().HasMaxLength(256);
            builder.Property(o => o.IsCard).HasDefaultValue(false);
            builder.Property(o => o.IsCourier).HasDefaultValue(false);
            builder.Property(o => o.IsCash).HasDefaultValue(false);
            builder.Property(o => o.IsSms).HasDefaultValue(false);
        }
    }
}
