using e_commerce_system.Models.Identity;

namespace e_commerce_system.Models
{
	public class RefreshToken
	{
	public	string Token { get; set; }
		public DateTime Expiration { get; set; }

		public User User { get; set; }

		public Guid UserId {  get; set; }

	}
}
