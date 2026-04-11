using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Attributes
{
	//this vaildation attribute for phone number 
	public class PhoneNumberAttribute:ValidationAttribute

	{

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if(value is not string phonenumber)
			{
				return new ValidationResult("invaild phone number formate");
			}

			var phoneNumberUtility = PhoneNumberUtil.GetInstance();

			try
			{

				var ParsedNumber = phoneNumberUtility.Parse(phonenumber, null); //can you assign defualt country code like +964
				if (!phoneNumberUtility.IsValidNumber(ParsedNumber))
				{

					return new ValidationResult("Invaild Phone Number ");
				}
			}
			catch (NumberParseException)
			{

				return new ValidationResult("Invaild Phone Number ");
			}

			return ValidationResult.Success;
		}
	}
}
