using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

            return RedirectToAction("SignIn");
        }
    }
}