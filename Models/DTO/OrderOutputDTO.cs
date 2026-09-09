namespace e_commerce_system.Models.DTO
{
    public class OrderOutputDTO
    {
        
		public string OrderNumber {  get; set; }

		public decimal TotalAmount { get; set; }

		public decimal SubTotal {  get; set; }

		public decimal ShippingAmount { get; set; }

		public string PaymentMethod { get; set; }

    }
}