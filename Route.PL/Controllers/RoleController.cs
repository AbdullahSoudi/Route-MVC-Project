using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.PL.ViewModels.AccountViewModels;

namespace Route.PL.Controllers
{
    public class RoleController(RoleManager<IdentityRole> roleManager) : Controller
    {
        #region Index
        public IActionResult Index(string search)
        {
            var query = roleManager.Roles.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(r => r.Name.ToLower().Contains(search.ToLower()));

            var roles = query.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return View(roles);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            return View(new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            });
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            return View(new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
                return View(roleViewModel);

            if (id != roleViewModel.Id)
                return BadRequest();

            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            role.Name = roleViewModel.Name;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Role Can't be updated");
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Role Can't be deleted");
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
                return View(roleViewModel);

            var result = await roleManager.CreateAsync(new IdentityRole
            {
                Name = roleViewModel.Name,
            });
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Role Can't be created");
            return View(roleViewModel);
        }
        #endregion
    }
}
