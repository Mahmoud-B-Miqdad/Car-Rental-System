namespace CarRentalSystem.Db.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public DateTime AvailableFromDate { get; set; }
        public DateTime AvailableToDate { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}