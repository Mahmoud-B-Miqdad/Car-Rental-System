using CarRentalSystem.Db.Entities;

namespace CarRentalSystem.Db.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> DoesEmailExistAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}