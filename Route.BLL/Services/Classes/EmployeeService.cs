using AutoMapper;
using Route.BLL.DTOs.Employee;
using Route.BLL.Services.Interfaces;
using Route.DAL.Models.EmployeeModule;
using Route.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) : IEmployeeService
    {
        public int CreateEmployee(EmployeeCreationDTO empDTO)
        {
            var employee = mapper.Map<Employee>(empDTO);
            return employeeRepository.Add(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = employeeRepository.GetById(id);

            if (employee is null)
                return false;

            employee.IsDeleted = true;
            return employeeRepository.Update(employee) > 0 ? true : false;
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees(bool withTracking = false)
        {
            var employees = employeeRepository.GetAll(withTracking);

            var employeesDTO = mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return employeesDTO;
        }

        public EmployeeDetailsDTO? GetEmployeeById(int id)
        {
            var employee = employeeRepository.GetById(id);

            return employee is null ? null : mapper.Map<EmployeeDetailsDTO>(employee);

        }

        public EmployeeUpdateDTO? GetEmployeeForUpdate(int id)
        {
            var employee = employeeRepository.GetById(id);
            return employee is null ? null : mapper.Map<EmployeeUpdateDTO>(employee);
        }

        public int UpdateEmployee(EmployeeUpdateDTO empDTO)
        {
            return employeeRepository.Update(mapper.Map<Employee>(empDTO));
        }
    }
}
