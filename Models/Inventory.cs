namespace Application.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; set; }
        public int Amount { get; set; }
    }
}
