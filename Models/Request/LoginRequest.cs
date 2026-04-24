using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.Request
{
	public class LoginRequest
	{
		[Required]

		public string identifier { get; set; }

		[Required]
		
		public string Password { get; set; }
	}
}
