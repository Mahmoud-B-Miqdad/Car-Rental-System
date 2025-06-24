namespace CarRentalSystem.Web.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public string Type { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public DateTime AvailableFromDate { get; set; }
        public DateTime AvailableToDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}