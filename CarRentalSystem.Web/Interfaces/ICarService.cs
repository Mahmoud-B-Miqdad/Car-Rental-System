using CarRentalSystem.Db.Entities;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Interfaces
{
    public interface ICarService
    {
        Task<List<CarViewModel>> GetAvailableCarsAsync();
        Task<List<CarViewModel>> SearchAvailableCarsAsync(SearchCarViewModel input);
        Task<Car?> GetCarByIdAsync(int carId);
    }
}