namespace Route.DAL.Data.Configurations
{
    public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(D => D.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(D => D.LastModificationOn)
                .HasComputedColumnSql("GETDATE()");
        }
    }
}
