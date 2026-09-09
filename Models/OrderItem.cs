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

		public DateTime CreatedAt { get; set; }= DateTime.Now;

		public decimal UnitPrice { get; set; }

		public decimal LineTotal { get; set; }



public OrderItem()
		{
		}

		public OrderItem(CartItem cartItem,decimal lineTotal)
		{
			ProductId = cartItem.ProductId;
			Quantity = cartItem.Quantity;
			UnitPrice = cartItem.UnitPrice;
			LineTotal = lineTotal;
			Quantity = cartItem.Quantity;
		
		}

		static public OrderItem FromCartItem(CartItem cartItem, decimal lineTotal)
		{
			return new OrderItem(cartItem, lineTotal);
		}


	}
}
