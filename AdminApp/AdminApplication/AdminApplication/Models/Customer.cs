namespace AdminApplication.Models
{
    public class Customer
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<Rentals>? rentals { get; set; }
    }
}
