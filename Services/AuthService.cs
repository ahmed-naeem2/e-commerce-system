using e_commerce_system.IServices;
using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_system.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		public AuthService(IUserService userService, UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			_userService = userService;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public async Task<bool> CheckUserExistsByEmailAsync(string email)
		{
			return await _userService.CheckIsEmailExistAsync(email);
		}

		public async Task<bool> CheckUserExistsByPhoneNumber(string phoneNumber)
		{
			return await _userService.CheckIsPhoneNumberExistAsync(phoneNumber);
		}

		public async Task<string> GenerateUserNameAsync(string UserName)
		{
			var NormalizeName = UserName.Replace(" ", "");
			string name;
			var random = new Random();
			do
			{
				name = NormalizeName + random.Next(1000, 9999);

			} while (await _userService.IsUserNameTaken(name));

			return name;
		}


		public async Task<IdentityResult?> CreatUserAsync(User user, string password) => await _userManager.CreateAsync(user, password);

		public async Task AddRoleToUserAsync(User user, string role) => await _userManager.AddToRoleAsync(user, role);

		public async Task <bool>IsEmail(string email)
		{
			return email.Contains("@")?true : false;
		}
	
		
			}
	}
