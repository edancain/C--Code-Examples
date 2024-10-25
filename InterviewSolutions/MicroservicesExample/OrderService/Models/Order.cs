
namespace Models{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}