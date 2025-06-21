using CarRentalSystem.Db.Entities;

namespace CarRentalSystem.Db.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}