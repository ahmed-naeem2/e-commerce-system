using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.Identity;
using e_commerce_system.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		private readonly IJwtService _jwtService;

		private readonly MainAppDbContet _context;
		public AuthService(IUserService userService, UserManager<User> userManager, RoleManager<Role> roleManager,IJwtService jwtService,MainAppDbContet context)
		{
			_userService = userService;
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtService = jwtService;
			_context = context;
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

			} while (await _userService.IsUserNameTakenAsync(name));

			return name;
		}


		public async Task<IdentityResult?> CreatUserAsync(User user, string password) => await _userManager.CreateAsync(user, password);

		public async Task AddRoleToUserAsync(User user, string role) => await _userManager.AddToRoleAsync(user, role);

	
			
		public async Task<bool> VaildatecredentialsAsync(User user, string password)
		{
			
			return await _userManager.CheckPasswordAsync(user, password);



		}

public		bool IsEmail(string identifier)
		{
			return identifier.Contains("@")?true:false;
		}

	public async	Task<AuthenticationResponse> LoginResponseAsync(User user)
		{
			var role = await _userService.GetRoleAsync(user);

			var authenticationRespone = _jwtService.GenrateJWt(user, role);
			var storedRerfreshToken=await _userService.GetRefreshTokenAsync(user); //if the user has refresh token in DB rplace it with new one 

			if (storedRerfreshToken != null)
			{
				storedRerfreshToken.Token = authenticationRespone.RefreshToken;
				storedRerfreshToken.Expiration=authenticationRespone.RefreshTokenExpiration;
				
				_context.RefreshTokens.Update(storedRerfreshToken);

				await _context.SaveChangesAsync();	

				return authenticationRespone;
			}

			RefreshToken refreshToken = new RefreshToken(
				authenticationRespone.RefreshToken,
				authenticationRespone.RefreshTokenExpiration,
				user.Id

				);
			_context.RefreshTokens.Add(refreshToken);
			
				await _context.SaveChangesAsync();
			
			

			return authenticationRespone;
		}
	}
	}
