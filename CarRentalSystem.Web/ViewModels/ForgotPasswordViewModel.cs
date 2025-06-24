using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Web.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}