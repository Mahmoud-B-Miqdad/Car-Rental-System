namespace CarRentalSystem.Db.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string DriverLicenseNumber { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

}