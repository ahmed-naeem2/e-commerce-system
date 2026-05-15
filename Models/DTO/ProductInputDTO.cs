using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.DTO
{
	public class ProductInputDTO
	{

		[MaxLength(60,ErrorMessage = " Product name must not exceed 60 characters.")]
		[Required(ErrorMessage ="Name is required")]
		public string Name { get; set; }
		[MaxLength(255,ErrorMessage = "Description must not exceed 255 characters.")]
		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		[Required]
		
		public string CategorieName { get; set; }

		[Required]
		public decimal Price { get; set; }

	


	}
}
