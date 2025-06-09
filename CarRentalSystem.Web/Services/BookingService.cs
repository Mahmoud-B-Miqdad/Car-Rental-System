using CarRentalSystem.Db.Entities;
using CarRentalSystem.Db.Interfaces;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICarService _carService;
        private readonly IUserService _userService;

        public BookingService(IBookingRepository bookingRepository, ICarService carService, IUserService userService)
        {
            _bookingRepository = bookingRepository;
            _carService = carService;
            _userService = userService;
        }

        public async Task<List<BookingViewModel>> GetUserBookingsAsync(string userEmail)
        {
            var bookings = await _bookingRepository.GetBookingsByUserEmailAsync(userEmail);

            return bookings.Select(b => new BookingViewModel
            {
                CarBrand = b.Car.Brand,
                CarModel = b.Car.Model,
                CarYear = b.Car.Year,
                PricePerDay = b.Car.PricePerDay,
                Location = b.Car.Location,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                TotalPrice = b.TotalPrice
            }).ToList();
        }

        public async Task<bool> ReserveCarAsync(int carId, string userEmail, DateTime startDate, DateTime endDate)
        {
            var car = await _carService.GetCarByIdAsync(carId);

            var user = await _userService.GetUserByEmailAsync(userEmail);

            if (user == null || car == null)
            {
                return false; 
            }

            bool isWithinAvailableDates =
                startDate >= car.AvailableFromDate &&
                endDate <= car.AvailableToDate;

            bool isOverlapping = car.Bookings.Any(b =>
                (startDate < b.EndDate && endDate > b.StartDate));

            if (!isWithinAvailableDates || isOverlapping)
                return false;

            var days = (endDate - startDate).Days;
            var totalPrice = days * car.PricePerDay;

            var booking = new Booking
            {
                CarId = carId,
                UserId = user.Id,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = totalPrice
            };

            await _bookingRepository.AddBookingAsync(booking);
            return true;
        }
    }
}