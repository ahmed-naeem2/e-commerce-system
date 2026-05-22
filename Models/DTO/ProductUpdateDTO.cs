using Microsoft.AspNetCore.HttpLogging;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.DTO
{
	public class ProductUpdateDTO
	{
		public string? Name { get; set; }

		public string? Description { get; set; }



		public string? CategorieName { get; set; }
		[Range(typeof(decimal),"0", "9999999999", ErrorMessage = "Price must be greater than or equal to 0.")]
		public decimal? Price { get; set; }

		public bool IsActive { get; set; } = true;

	}
}
