using CarRentalSystem.Db.Entities;
using CarRentalSystem.Db.Interfaces;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace CarRentalSystem.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _userRepository.DoesEmailExistAsync(email);
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
            await _userRepository.SaveChangesAsync();
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

        public async Task SaveChangesAsync()
        {
            await _userRepository.SaveChangesAsync();
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