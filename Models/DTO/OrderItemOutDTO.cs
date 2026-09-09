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
    
    public OrderItemOutputDTO()
        {
        }

        public OrderItemOutputDTO(OrderItem orderItem)
        {
            ProductId = orderItem.ProductId;
            Quantity = orderItem.Quantity;
            ProductName = orderItem.product?.Name ?? string.Empty;
            UnitPrice = orderItem.UnitPrice;
            imageUrl = orderItem.product?.Images.FirstOrDefault()?.ImagePath ?? string.Empty;
        }

    static    public OrderItemOutputDTO FromOrderItem(OrderItem orderItem)
        {
            return new OrderItemOutputDTO(orderItem);
        }
    }
}