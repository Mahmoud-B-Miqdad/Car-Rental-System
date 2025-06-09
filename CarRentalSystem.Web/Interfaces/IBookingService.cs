using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingViewModel>> GetUserBookingsAsync(string userEmail);
        Task<bool> ReserveCarAsync(int carId, string userEmail, DateTime startDate, DateTime endDate);
    }
}