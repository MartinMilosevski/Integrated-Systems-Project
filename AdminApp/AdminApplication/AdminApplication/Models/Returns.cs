namespace AdminApplication.Models
{
    public class Returns
    {
        public Guid Id { get; set; }
        public int Additional_fee { get; set; }
        public Guid? RentalId { get; set; }
        public Rentals? Rental { get; set; }
        public string? CustomerName { get; set; }
    }
}
