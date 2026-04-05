using e_commerce_system.Enum;
using e_commerce_system.Models.Identity;
using Microsoft.OpenApi.Writers;

namespace e_commerce_system.Models
{
	public class Cart
	{
		public Guid ID { get; set; }


		public double TotalAmount { get; set; }

		public Guid UserId { get; set; }
		public User? user	{ get; set; }

		public CartStatus Status { get; set; } = CartStatus.Active;

		public string? SessionId { get; set; }

		public DateTime UpdatedAt { get; set; }

		public ICollection<CartItem>Items { get; set; }=new List<CartItem>();

	



	}
}
