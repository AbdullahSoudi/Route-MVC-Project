using Route.BLL.DTOs.Department;

namespace Route.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(DepartmentCreationDTO deptDTO);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDTO> GetAllDepartments();
        DepartmentDetailsDTO? GetDepartmentById(int id);
        int UpdateDepartment(int id, DepartmentUpdateDTO deptDTO);
    }
}