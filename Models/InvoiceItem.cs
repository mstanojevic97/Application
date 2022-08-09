namespace Application.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; set; }
        public int Amount { get; set; }
    }
}
