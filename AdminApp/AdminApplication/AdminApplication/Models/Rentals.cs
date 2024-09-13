namespace AdminApplication.Models
{
    public class Rentals
    {
        public Guid? CarId { get; set; }
        public Car? car { get; set; }
        public string? CustomerId { get; set; }
        public Customer? customer { get; set; }
        public DateOnly Date_from { get; set; }
        public DateOnly Date_on { get; set; }
        public int FullPrice { get; set; }
    }
}
