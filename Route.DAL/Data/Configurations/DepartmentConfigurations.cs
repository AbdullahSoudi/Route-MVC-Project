using Route.DAL.Models.DepartmentModule;

namespace Route.DAL.Data.Configurations
{
    public class DepartmentConfigurations : BaseEntityConfigurations<Department>, IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id)
                .UseIdentityColumn(10, 10);

            builder.Property(D => D.Name)
               .HasColumnType("varchar(20)");

            builder.Property(D => D.Code)
                .HasColumnType("varchar(20)");

            builder.HasMany(D => D.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            base.Configure(builder);
        }
    }
}
