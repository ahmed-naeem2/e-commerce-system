using e_commerce_system.IServices;
using e_commerce_system.Models.Identity;
using e_commerce_system.Models.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace e_commerce_system.Services
{
	public class JwtService:IJwtService
	{
		private readonly IConfiguration _configuration;

		public JwtService(IConfiguration configuration)

		{
			_configuration = configuration;
		}

		public AuthenticationResponse GenrateJWt(User user)
		{
			DateTime Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"]));

			Claim[] claims = new Claim[] {
				new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),//JWt Uniqe ID

new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),

				new Claim(ClaimTypes.NameIdentifier,user.Email.ToString()),
new Claim(ClaimTypes.Name,user.PersonName.ToString()),



			};
			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

			SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken TokenGenrator = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: Expiration,
				signingCredentials: credentials


				);

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

			string token = tokenHandler.WriteToken(TokenGenrator);

			return new AuthenticationResponse
			{
				Token = token,

				TokenExpiration = Expiration,
			
				RefreshToken = GenerateRefreshToken(),
				RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:Expiration_Minutes"]))
			};
		}

		

		private string GenerateRefreshToken()
		{

			byte[] bytes = new byte[64];

			var randomNumber = RandomNumberGenerator.Create();
			randomNumber.GetBytes(bytes);

			return Convert.ToBase64String(bytes);

		}
	}
}
