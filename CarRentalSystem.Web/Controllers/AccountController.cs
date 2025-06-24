using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalSystem.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!await _userService.IsEmailUniqueAsync(model.Email))
            {
                ModelState.AddModelError("Email", "The email address is already in use.");
                return View(model);
            }

            await _userService.RegisterUserAsync(model);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.ValidateUserCredentialsAsync(model);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userService.GetUserByEmailAsync(userEmail);

            if (user == null)
                return RedirectToAction("Login");

            var model = new EditProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                City = user.City,
                Country = user.Country
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _userService.UpdateUserAsync(model);

            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to update user.";
                return View(model);
            }

            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToAction("Edit");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.SendPasswordResetLinkAsync(model.Email, Url);

            if (result)
            {
                TempData["SuccessMessage"] = "Password reset link has been sent to your email.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Email not found.");
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);

            if (result)
            {
                TempData["SuccessMessage"] = "Password has been reset successfully.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Invalid reset token or email.");
            return View(model);
        }
    }
}