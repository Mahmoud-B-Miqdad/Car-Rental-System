using CarRentalSystem.Db.Entities;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        Task<bool> SendPasswordResetLinkAsync(string email, IUrlHelper urlHelper);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}