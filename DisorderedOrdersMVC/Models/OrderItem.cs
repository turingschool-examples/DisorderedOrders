namespace DisorderedOrdersMVC.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Item { get; set; }
    }
}
