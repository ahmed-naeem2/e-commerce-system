using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_system.IServices
{
	public interface IAuthService
	{
		Task<bool> CheckUserExistsByEmailAsync(string email);
		Task<bool> CheckUserExistsByPhoneNumber(string phoneNumber);

		Task<string> GenerateUserName(string UserName);
		Task<IdentityResult> CreatUserAsync(User user, string Password);

		Task AddRoleToUserAsync(User user, string role);	

	}
}
