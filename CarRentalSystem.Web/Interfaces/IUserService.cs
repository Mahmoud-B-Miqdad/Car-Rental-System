using CarRentalSystem.Db.Entities;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<bool> IsEmailUniqueAsync(string email);
        Task RegisterUserAsync(RegisterViewModel model);
        Task<User?> ValidateUserCredentialsAsync(LoginViewModel model);
        Task<bool> UpdateUserAsync(EditProfileViewModel model);
    }
}