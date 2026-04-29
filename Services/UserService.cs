using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Services
{
	public class UserService:IUserService
	{

		private readonly UserManager<User> _userManager;
		private readonly MainAppDbContet _context;
	
		public UserService(UserManager<User> userManager,MainAppDbContet context	)
		{
			_userManager = userManager;
			_context = context;
		}

	public  async Task<bool> CheckIsEmailExistAsync(string email) => await _userManager.Users.AnyAsync(u => u.Email.ToLower() == email);

		

public async Task<User?> FindUserByEmailAsync(string email)=>   await _userManager.FindByEmailAsync(email);





		public async	Task<bool> CheckIsPhoneNumberExistAsync(string phoneNumber)
		{
			return await _userManager.Users.AnyAsync(u=>u.PhoneNumber==phoneNumber);
		}

	public async Task<bool> IsUserNameTakenAsync(string userName)=>await _userManager.Users.AnyAsync(u=>u.UserName==userName);

		public async Task<User?> FindUserAsync(string EmailOrPhoneNumber)
		{
			return await _userManager.Users.FirstOrDefaultAsync(u=>
			
			u.Email== EmailOrPhoneNumber || u.PhoneNumber== EmailOrPhoneNumber);
		}

		public async Task<string>GetRoleAsync(User user)
		{
			return (await _userManager.GetRolesAsync(user)).FirstOrDefault();

			
		}

		public async Task<RefreshToken> GetRefreshTokenAsync(User user)
		{
		return	await _context.RefreshTokens.Where(x=>x.UserId==user.Id).FirstOrDefaultAsync();
		}
	}
}
