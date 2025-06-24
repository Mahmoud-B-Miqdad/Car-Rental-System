using CarRentalSystem.Db.Entities;
using CarRentalSystem.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Db.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars
                .Include(c => c.Bookings)
                .ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Car>> GetFilteredCarsAsync(DateTime startDate, DateTime endDate, string? location)
        {
            return await _context.Cars
                .Where(c =>
                    c.AvailableFromDate <= startDate &&
                    c.AvailableToDate >= endDate &&
                    (string.IsNullOrEmpty(location) || c.Location.ToLower().Contains(location.ToLower()))
                )
                .ToListAsync();
        }
    }
}