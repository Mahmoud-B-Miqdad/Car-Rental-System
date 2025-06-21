using CarRentalSystem.Db.Entities;
using CarRentalSystem.Db.Interfaces;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<CarViewModel>> GetAvailableCarsAsync()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            var now = DateTime.UtcNow;

            return cars.Select(car => new CarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Type = car.Type,
                Location = car.Location,
                PricePerDay = car.PricePerDay,
                AvailableFromDate = car.AvailableFromDate,
                AvailableToDate = car.AvailableToDate,

                IsAvailable = car.AvailableFromDate <= now && car.AvailableToDate >= now
            }).ToList();
        }

        public async Task<List<CarViewModel>> SearchAvailableCarsAsync(SearchCarViewModel input)
        {
            var cars = await _carRepository.GetAllCarsAsync();

            var filteredCars = await _carRepository.GetFilteredCarsAsync(input.StartDate, input.EndDate, input.Location);

            var AvailableCars = filteredCars.Select(c => new CarViewModel
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                Type = c.Type,
                Location = c.Location,
                PricePerDay = c.PricePerDay,
                AvailableFromDate = c.AvailableFromDate,
                AvailableToDate = c.AvailableToDate,
                IsAvailable = true
            }).ToList();

            return AvailableCars;
        }

        public async Task<Car?> GetCarByIdAsync (int carId)
        {
            return await _carRepository.GetCarByIdAsync(carId);
        }
    }
}