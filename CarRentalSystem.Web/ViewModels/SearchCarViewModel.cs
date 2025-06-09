using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Web.ViewModels
{
    public class SearchCarViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? Location { get; set; }
    }
}