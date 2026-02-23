using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.BLL.Services.EmailSender;
using Route.DAL.Models;
using Route.DAL.Models.Shared;
using Route.PL.ViewModels.AccountViewModels;

namespace Route.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var user = new ApplicationUser
            {
                Email = viewModel.Email,
                UserName = viewModel.Username,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
            };

            var result = await userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(viewModel);
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user is not null)
            {
                var flag = await userManager.CheckPasswordAsync(user, viewModel.Password);
                if (flag)
                {
                    var result = await signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false);
                    if (result.IsNotAllowed)
                        ModelState.AddModelError("", "Account is not allowed");
                    if (result.IsLockedOut)
                        ModelState.AddModelError("", "Account is locked");
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Login");
            return View(viewModel);
        }
        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordURL(ForgetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(ForgetPassword), viewModel);

            var user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user is not null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var url = Url.Action("ResetPassword", "Account", new { viewModel.Email, token }, Request.Scheme);
                var email = new Email
                {
                    To = viewModel.Email,
                    Subject = "Reset Yout Password",
                    Body = url
                };

                emailSender.SendEmail(email);
                return RedirectToAction("CheckYourInbox");
            }

            ModelState.AddModelError("", "Invalid, Try again later");
            return View(nameof(ForgetPassword), viewModel);
        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await userManager.ResetPasswordAsync(user, token, viewModel.NewPassword);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));
                }
            }
            ModelState.AddModelError("", "Invalid Operation, Try again!");
            return View(viewModel);
        }
        #endregion
    }
}
