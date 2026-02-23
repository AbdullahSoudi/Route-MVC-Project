using Route.DAL.Models.EmployeeModule;

namespace Route.DAL.Data.Configurations
{
    public class EmployeeConfigurations : BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name)
               .HasColumnType("varchar(50)");

            builder.Property(e => e.Address)
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Salary)
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.Gender)
                .HasConversion(empGender => empGender.ToString(),
                gender => (Gender)Enum.Parse(typeof(Gender), gender));

            builder.Property(e => e.EmployeeType)
                .HasConversion(empType => empType.ToString(),
                type => Enum.Parse<EmployeeTypes>(type));

            base.Configure(builder);
        }
    }
}
