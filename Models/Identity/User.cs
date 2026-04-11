using Microsoft.AspNetCore.Identity;

namespace e_commerce_system.Models.Identity
{
	public class User:IdentityUser<Guid>
	{
		public string PersonName { get; set; }
		public String Address { get; set; }

		public string City { get; set; }



	public	ICollection<Order> Orders { get; set; } = new List<Order>();
	public	ICollection<Cart>Carts { get; set; }=new List<Cart>();	

	}
}
