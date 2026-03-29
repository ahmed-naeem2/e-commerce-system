namespace e_commerce_system.Models
{
	public class OrderItem
	{
		public Guid ID { get; set; }

		public Guid ProductId { get; set; }

		public Product? product { get; set; }

		public Guid OrderId { get; set; }

		public Order? order { get; set; }

		public int Quantity { get; set; } = 1;

		public DateTime CreatedAt { get; set; }

		public double UnitPrice { get; set; }

		public double LineTotal { get; set; }






	}
}
