namespace e_commerce_system.Models.Response
{
	public class AuthenticationResponse
	{
		public string Token { get; set; }

		public DateTime TokenExpiration {  get; set; }

		public string RefreshToken { get; set; }

		public DateTime RefreshTokenExpiration { get; set; }

	}
}
