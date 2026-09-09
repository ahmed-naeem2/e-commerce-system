namespace e_commerce_system.Models.DTO
{
    public class OrderItemOutputDTO
    {
public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string imageUrl { get; set; } = string.Empty;

        public decimal TotalPrice => Quantity * UnitPrice;        
    
    }
}