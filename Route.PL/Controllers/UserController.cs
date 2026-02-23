using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.DAL.Models;
using Route.PL.ViewModels.AccountViewModels;

namespace Route.PL.Controllers
{
    public class UserController(UserManager<ApplicationUser> userManager) : Controller
    {
        #region Index
        public async Task<IActionResult> Index(string search)
        {
            var query = userManager.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(u => u.Email.ToLower().Contains(search.ToLower()));

            var users = query.Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
            }).ToList();

            foreach (var user in users)
            {
                user.Roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.Id));
            }

            return View(users);
        }
        #endregion
        #region Details
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return BadRequest();

            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var userVm = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await userManager.GetRolesAsync(user)
            };

            return View(userVm);
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
                return BadRequest();

            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var userVm = new UserEditViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await userManager.GetRolesAsync(user)
            };

            return View(userVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserEditViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return View(userViewModel);

            if (userViewModel.Id != id)
                return BadRequest();

            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "user can't be updated");
            return View(userViewModel);
        }
        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "User Can't be deleted");
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
