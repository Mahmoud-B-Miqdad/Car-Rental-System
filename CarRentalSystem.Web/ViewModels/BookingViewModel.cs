namespace CarRentalSystem.Web.ViewModels
{
    public class BookingViewModel
    {
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public int CarYear { get; set; }
        public string Location { get; set; }
        public decimal PricePerDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}