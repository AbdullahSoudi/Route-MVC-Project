using Route.BLL.DTOs.Department;
using Route.BLL.Factories;
using Route.BLL.Services.Interfaces;
using Route.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.Services.Classes
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = departmentRepository.GetAll();

            return departments.Select(d => d.ToDepartmentsDTO());
        }

        public DepartmentDetailsDTO? GetDepartmentById(int id)
        {
            var department = departmentRepository.GetById(id);

            return department is null ? null : department.ToDepartmentDetailsDTO();
        }

        public int AddDepartment(DepartmentCreationDTO deptDTO)
        {
            return departmentRepository.Add(deptDTO.ToEntity());
        }

        public int UpdateDepartment(int id, DepartmentUpdateDTO deptDTO)
        {
            var departmentFromDB = departmentRepository.GetById(id);
            deptDTO.ToEntity(departmentFromDB);

            return departmentRepository.Update(departmentFromDB);
        }

        public bool DeleteDepartment(int id)
        {
            var dept = departmentRepository.GetById(id);
            if (dept is null) return false;

            int result = departmentRepository.Delete(dept);
            return result > 0 ? true : false;
        }
    }
}
