using Route.DAL.Data.Contexts;
using Route.DAL.Models.EmployeeModule;
using Route.DAL.Repositories.Interfaces;

namespace Route.DAL.Repositories.Classes
{
    public class EmployeeRepository(AppDbContext context) : GenericRepository<Employee>(context), IEmployeeRepository
    {

    }
}
