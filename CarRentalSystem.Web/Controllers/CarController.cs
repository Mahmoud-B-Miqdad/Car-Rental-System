using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Web.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAvailableCarsAsync();
            return View(cars);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchCarViewModel input)
        {
            if (!ModelState.IsValid)
            {
                var cars = await _carService.GetAvailableCarsAsync();
                return View("Index", cars);
            }

            var filteredCars = await _carService.SearchAvailableCarsAsync(input);
            return View("Index", filteredCars);
        }
    }
}