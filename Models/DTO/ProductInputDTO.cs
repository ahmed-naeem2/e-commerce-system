using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.DTO
{
	public class ProductInputDTO
	{

		[MaxLength(200,ErrorMessage = " Product name must not exceed 60 characters.")]
		[Required(ErrorMessage ="Name is required")]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		[Required]
		
		public string CategorieName { get; set; }

		[Required]
		[Range(0,double.MaxValue,ErrorMessage = "Price must be greater than or equal to 0. ")]
		public decimal Price { get; set; }

	


	}
}
