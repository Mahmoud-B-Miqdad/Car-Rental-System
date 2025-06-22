using CarRentalSystem.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalSystem.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICarService _carService;

        public BookingController(IBookingService bookingService, ICarService carService)
        {
            _bookingService = bookingService;
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bookings = await _bookingService.GetUserBookingsAsync(userEmail);
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(int carId, DateTime startDate, DateTime endDate)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to reserve a car.";
                return RedirectToAction("Index", "Car");
            }

            var result = await _bookingService.ReserveCarAsync(carId, userEmail, startDate, endDate );

            if (result)
            {
                TempData["SuccessMessage"] = "Car reserved successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to reserve the car. It might have been already booked.";
            }

            return RedirectToAction("Index", "Car");
        }
    }
}