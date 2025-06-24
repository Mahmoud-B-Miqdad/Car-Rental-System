using CarRentalSystem.Db.Entities;

namespace CarRentalSystem.Db.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetBookingsByUserEmailAsync(string userEmail);
        Task AddBookingAsync(Booking booking);
    }
}