using CarRentalSystem.Db.Entities;
using CarRentalSystem.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Db.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CarRentalDbContext _context;

        public BookingRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetBookingsByUserEmailAsync(string userEmail)
        {
            return await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.User)
                .Where(b => b.User.Email == userEmail)
                .ToListAsync();
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }
    }
}