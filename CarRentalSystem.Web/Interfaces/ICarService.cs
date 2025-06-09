using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Interfaces
{
    public interface ICarService
    {
        Task<List<CarViewModel>> GetAllCarsAsync();
    }
}