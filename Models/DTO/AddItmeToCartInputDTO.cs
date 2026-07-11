using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.DTO
{
	public class AddItemToCartDTO
	{
		[Required(ErrorMessage = "ProductId is required.")]
		public Guid ProductId { get; set; }
		
		    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]

		public int Quantity { get; set; }
	}
}
