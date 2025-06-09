namespace CarRentalSystem.Db.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Brand { get; set; } 
        public string Model { get; set; } 
        public int Year { get; set; }

        public string Type { get; set; } = null!;  
        public string Location { get; set; } = null!;   

        public decimal PricePerDay { get; set; }

        public DateTime AvailableFromDate { get; set; }
        public DateTime AvailableToDate { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}