using CarRentalSystem.Db.Entities;

namespace CarRentalSystem.Db.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCarsAsync();
    }
}