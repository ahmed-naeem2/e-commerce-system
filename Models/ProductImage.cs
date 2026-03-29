namespace e_commerce_system.Models
{
	public class ProductImage
	{
		public Guid Id { get; set; }

		public string ImagePath { get; set; }

		public Guid ProductId { get; set; }

		public Product? product { get; set; }


	}
}
