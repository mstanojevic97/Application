namespace Application.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
    }
}
