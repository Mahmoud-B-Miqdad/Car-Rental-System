namespace CarRentalSystem.Web.ViewModels
{
    public class BookingViewModel
    {
        public string CarBrand { get; set; } = null!;
        public string CarModel { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}