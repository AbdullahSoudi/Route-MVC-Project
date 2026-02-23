using Route.DAL.Models.EmployeeModule;
using Route.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.DAL.Models.DepartmentModule
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }

        public ICollection<Employee> Employees = new HashSet<Employee>();
    }
}
