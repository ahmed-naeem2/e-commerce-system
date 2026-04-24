using e_commerce_system.Models.Identity;
using e_commerce_system.Models.Response;

namespace e_commerce_system.IServices
{
	public interface  IJwtService
	{
		public AuthenticationResponse GenrateJWt(User user);
	}
}
