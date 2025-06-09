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

        public async Task<List<CarViewModel>> GetAllCarsAsync()
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
    }
}