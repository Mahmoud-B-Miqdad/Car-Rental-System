namespace CarRentalSystem.Db.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        public User User { get; set; } = null!;
        public Car Car { get; set; } = null!;
    }
}