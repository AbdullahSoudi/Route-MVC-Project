using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Route.BLL.DTOs;
using Route.BLL.DTOs.Department;
using Route.BLL.Services.Interfaces;
using Route.PL.ViewModels.DepartmentViewModels;

namespace Route.PL.Controllers
{
    public class DepartmentsController(IDepartmentService _departmentService, ILogger<DepartmentsController> _logger, IWebHostEnvironment _environment, IMapper _mapper) : Controller
    {
        private readonly IDepartmentService departmentService = _departmentService;
        private readonly ILogger<DepartmentsController> logger = _logger;
        private readonly IWebHostEnvironment environment = _environment;
        private readonly IMapper mapper = _mapper;

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = departmentService.GetAllDepartments();
            return View(departments);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = departmentService.AddDepartment(mapper.Map<DepartmentCreationDTO>(viewModel));
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department can't be created");
                    }
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    logger.LogError(ex.Message);

                }
            }
            return View(viewModel);
        }
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var department = departmentService.GetDepartmentById(id);
            if (department is null)
                return NotFound();

            return View(department);
        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = departmentService.GetDepartmentById(id);
            if (department is null)
                return NotFound();

            var departmentViewModel = mapper.Map<DepartmentViewModel>(department);

            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var updatedDepartmentDTO = mapper.Map<DepartmentUpdateDTO>(viewModel);

                int result = departmentService.UpdateDepartment(id, updatedDepartmentDTO);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department can't be updated");
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                logger.LogError(ex.Message);
            }
            return View(viewModel);
        }
        #endregion
        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = departmentService.DeleteDepartment(id);
                if (deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department isn't deleted");
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                logger.LogError(ex.Message);
                return View("ErrorView", ex.Message);
            }
            return RedirectToAction(nameof(Index), ModelState);
        }
        #endregion
    }
}
