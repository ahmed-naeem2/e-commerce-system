using e_commerce_system.IServices;
using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Services
{
	public class UserService:IUserService
	{

		private readonly UserManager<User> _userManager;

		public UserService(UserManager<User> userManager	)
		{
			_userManager = userManager;
		}

	public  async Task<bool> CheckIsEmailExistAsync(string email) => await _userManager.Users.AnyAsync(u => u.Email.ToLower() == email);

		

public async Task<User?> FindUserByEmailAsync(string email)
		{
			var user=await _userManager.FindByEmailAsync(email);

			if (user == null)
				return null;

			return user;
		}

	public async	Task<bool> CheckIsPhoneNumberExistAsync(string phoneNumber)
		{
			return await _userManager.Users.AnyAsync(u=>u.PhoneNumber==phoneNumber);
		}

	public async Task<bool> IsUserNameTaken(string userName)=>await _userManager.Users.AnyAsync(u=>u.UserName==userName);



		
	}
}
