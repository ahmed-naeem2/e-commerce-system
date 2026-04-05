namespace e_commerce_system.Models
{
	public class CartItem
	{

		public Guid ID { get; set; }

		public Guid ProductId { get; set; }
	

		public Product? product { get; set; }

		public Guid CartId { get; set; }

		public Cart? cart { get; set; }

		public int Quantity { get; set; } = 1;

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public decimal UnitPrice { get; set; }

	

	}
}
