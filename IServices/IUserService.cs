using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_system.IServices
{
	public interface IUserService
	{
		Task<bool> CheckIsEmailExistAsync(string email);
		Task <User?>FindUserByEmailAsync(string email);

		Task<bool>CheckIsPhoneNumberExistAsync (string phoneNumber);
		Task<bool>IsUserNameTaken(string userName);
	}
}
