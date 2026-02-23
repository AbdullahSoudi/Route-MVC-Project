using Microsoft.AspNetCore.Mvc;
using Route.BLL.DTOs.Employee;
using Route.BLL.Services.Interfaces;
using System;

namespace Route.PL.Controllers
{
    public class EmployeeController(IEmployeeService employeeService) : Controller
    {
        #region Index
        public IActionResult Index()
        {
            var employees = employeeService.GetAllEmployees(false);
            return View(employees);
        }
        #endregion
        #region Details
        public IActionResult Details(int id)
        {
            var employee = employeeService.GetEmployeeById(id);
            return View(employee);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreationDTO empDTO)
        {
            if (!ModelState.IsValid)
                return View(empDTO);

            int result = employeeService.CreateEmployee(empDTO);

            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Employee Can't be created");
                return View(result);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = employeeService.GetEmployeeForUpdate(id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeUpdateDTO empDTO)
        {
            if (!ModelState.IsValid)
                return View(empDTO);

            int result = employeeService.UpdateEmployee(empDTO);
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Can't edit employee");
                return View(empDTO);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool result = employeeService.DeleteEmployee(id);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "can't delete employee");
                return RedirectToAction(nameof(Index), ModelState);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
