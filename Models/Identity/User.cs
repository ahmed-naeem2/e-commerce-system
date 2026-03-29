using Microsoft.AspNetCore.Identity;

namespace e_commerce_system.Models.Identity
{
	public class User:IdentityUser<Guid>
	{
		public String Address { get; set; }

		ICollection<Order> Orders { get; set; } = new List<Order>();
		ICollection<Cart>Carts { get; set; }=new List<Cart>();	

	}
}
