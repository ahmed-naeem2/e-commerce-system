using e_commerce_system.Models.Identity;
using e_commerce_system.Models.Response;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_system.IServices
{
	public interface IAuthService
	{
		Task<bool> CheckUserExistsByEmailAsync(string email);
		Task<bool> CheckUserExistsByPhoneNumber(string phoneNumber);

		Task<string> GenerateUserNameAsync(string UserName);
		Task<IdentityResult> CreatUserAsync(User user, string Password);

		Task AddRoleToUserAsync(User user, string role);	
		Task<bool>IsEmail(string email);

		Task<bool> Vaildatecredentials(bool IsEmail,string identifi)



	}
}
