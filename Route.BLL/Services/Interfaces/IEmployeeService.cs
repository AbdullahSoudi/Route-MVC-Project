using Route.BLL.DTOs.Department;
using Route.BLL.DTOs.Employee;

namespace Route.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        int CreateEmployee(EmployeeCreationDTO empDTO);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDTO> GetAllEmployees(bool withTracking);
        EmployeeDetailsDTO? GetEmployeeById(int id);
        public EmployeeUpdateDTO? GetEmployeeForUpdate(int id);
        int UpdateEmployee(EmployeeUpdateDTO empDTO);
    }
}