using e_commerce_system.Models.Request;
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
		public ICollection<RefreshToken> refreshTokens { get; set; }=new List<RefreshToken>();	
		public User() { }
		public User(RegistrationReq req,string userName)
		{
			PersonName = req.name;
				Address = req.Address;
			City = req.city;
			Email = req.email;
			PhoneNumber = req.PhoneNumber;
			UserName = userName;
		}

	}
}
