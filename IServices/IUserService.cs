using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_system.IServices
{
	public interface IUserService
	{
		Task<bool> CheckIsEmailExistAsync(string email);
		Task <User?>FindUserByEmailAsync(string email);

		Task<bool>CheckIsPhoneNumberExistAsync (string phoneNumber);
		Task<bool>IsUserNameTakenAsync(string userName);
		Task<User?> FindUserAsync(string EmailOrPhoneNumber); //the value may be Email or Phone Number 
		Task<string> GetRoleAsync(User user);
	}
}
