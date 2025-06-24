using CarRentalSystem.Db.Entities;

namespace CarRentalSystem.Db.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car?> GetCarByIdAsync(int id);
        Task<List<Car>> GetFilteredCarsAsync(DateTime startDate, DateTime endDate, string? location);
    }
}