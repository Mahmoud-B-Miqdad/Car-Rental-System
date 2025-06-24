using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; } = null!;

        [Display(Name = "Address Line 2")]
        public string? AddressLine2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; } = null!;

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; } = null!;

        [Required]
        [Display(Name = "Driver's License Number")]
        public string DriverLicenseNumber { get; set; } = null!;
    }
}