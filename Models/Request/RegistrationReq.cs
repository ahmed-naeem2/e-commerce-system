using e_commerce_system.Attributes;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.Request
{
	public class RegistrationReq
	{
		[Required (ErrorMessage = "Name Is Required ")]
		public string name {  get; set; }
		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress(ErrorMessage ="Please Enter Valid Email Address")]
		public string email { get; set; }

		[Required(ErrorMessage = "Phone Number Is Required")]
		[PhoneNumber]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$",
        ErrorMessage = "Password must be at least 6 characters and include uppercase, lowercase, number, and special character."
    )]
		public string Password { get; set; }


		[Required(ErrorMessage = "City Is Required")]
		public string city { get; set; }

		[Required(ErrorMessage = "Address Is Required")]

		public string Address { get; set; }	


	}
}
