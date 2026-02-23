using Route.BLL.DTOs.Department;
using Route.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.Factories
{
    public static class DepartmentFactory
    {
        public static DepartmentDTO ToDepartmentsDTO(this Department d)
        {
            return new DepartmentDTO
            {
                DeptId = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                DateOfCreation = d.CreatedOn
            };
        }

        public static DepartmentDetailsDTO ToDepartmentDetailsDTO(this Department department)
        {
            return new DepartmentDetailsDTO
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                DateOfCreation = department.CreatedOn,
                LastModificationBy = department.LastModificationBy,
                LastModificationOn = department.LastModificationOn,
                IsDeleted = department.IsDeleted
            };
        }

        public static Department ToEntity(this DepartmentCreationDTO deptDTO)
        {
            return new Department
            {
                Name = deptDTO.Name,
                Code = deptDTO.Code,
                Description = deptDTO.Description,
                CreatedOn = deptDTO.DateOfCreation,
            };
        }
        public static void ToEntity(this DepartmentUpdateDTO deptDTO, Department oldDepartment)
        {
            oldDepartment.Name = deptDTO.Name;
            oldDepartment.Code = deptDTO.Code;
            oldDepartment.Description = deptDTO.Description;
            oldDepartment.CreatedOn = deptDTO.DateOfCreation;
        }

    }
}
