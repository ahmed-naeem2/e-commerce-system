using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.DTO
{
	public class CategorieInputDTO
	{
		[Required(ErrorMessage ="The name is required ")]
		[MaxLength(100,ErrorMessage = "Categorie Name must not exceed 100 characters.")]
		public string Name { get; set; }


	}
}
