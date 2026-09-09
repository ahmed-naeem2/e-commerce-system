namespace e_commerce_system.Models.DTO
{
    public class OrderOutputDTO
    {
        
		public string OrderNumber {  get; set; }

		public decimal TotalAmount { get; set; }

		public decimal SubTotal {  get; set; }

		public decimal ShippingAmount { get; set; }

		public string PaymentMethod { get; set; }

        public List<OrderItemOutputDTO> OrderItems { get; set; } = new List<OrderItemOutputDTO>();

        public OrderOutputDTO()
        {
        }

        public OrderOutputDTO(Order order)
        {
            OrderNumber = order.OrderNumber;
            TotalAmount = order.TotalAmount;
            SubTotal = order.SubTotal;
            ShippingAmount = order.ShippingAmount;
            PaymentMethod = order.PaymentMethod;
            OrderItems = order.Items.Select(oi => OrderItemOutputDTO.FromOrderItem(oi)).ToList();
            
        }
        static public OrderOutputDTO FromOrder(Order order)
        {
            return new OrderOutputDTO(order);
            
        }

    }
}