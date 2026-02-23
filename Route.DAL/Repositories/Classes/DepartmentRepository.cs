using Route.DAL.Data.Contexts;
using Route.DAL.Models.DepartmentModule;
using Route.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Route.DAL.Repositories.Classes
{
    public class DepartmentRepository(AppDbContext _context) : GenericRepository<Department>(_context), IDepartmentRepository
    {

    }
}
