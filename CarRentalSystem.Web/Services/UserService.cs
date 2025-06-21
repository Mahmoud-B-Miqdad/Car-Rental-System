using CarRentalSystem.Db.Entities;
using CarRentalSystem.Db.Interfaces;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace CarRentalSystem.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _userRepository.EmailExistAsync(email);
        }
        public async Task RegisterUserAsync(RegisterViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = HashPassword(model.Password), 
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                City = model.City,
                Country = model.Country,
                DriverLicenseNumber = model.DriverLicenseNumber
            }; 

            await _userRepository.AddUserAsync(user);
        }

        public async Task<User?> ValidateUserCredentialsAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.Email);

            if (user != null && VerifyPassword(model.Password, user.Password))
            {
                return user;
            }
            return null;
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> UpdateUserAsync(EditProfileViewModel model)
        {
            var user = await GetUserByEmailAsync(model.Email);
            if (user == null) return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.AddressLine1 = model.AddressLine1;
            user.AddressLine2 = model.AddressLine2;
            user.City = model.City;
            user.Country = model.Country;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> SendPasswordResetLinkAsync(string email, IUrlHelper urlHelper)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
                return false;

            var token = Guid.NewGuid().ToString();
            var resetLink = urlHelper.Action(
                action: "ResetPassword",
                controller: "Account",
                values: new { email, token },
                protocol: "https"
            );

            await _emailService.SendEmailAsync(
                toEmail: email,
                subject: "Reset Your Password",
                body: $"Click <a href=\"{resetLink}\">here</a> to reset your password."
            );
            return true;
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
                return false;

            user.Password = HashPassword(newPassword);
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == storedPassword;
        }
    }
}