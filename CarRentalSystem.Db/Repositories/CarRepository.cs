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
    }
}