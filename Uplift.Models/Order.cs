namespace Uplift.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer CustomerDetail { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}