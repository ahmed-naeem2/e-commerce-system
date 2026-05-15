using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.DTO
{
	public class ProductOutputDTO
	{
		
		public string Name { get; set; }
		
		public string Description { get; set; }

	

		public string CategorieName { get; set; }

		public decimal Price { get; set; }

		public ProductOutputDTO() { }

		public ProductOutputDTO(Product product,string categorieName)
		{
			Name=product.Name;
			Description=product.Description;
			Price=product.Price;
			CategorieName=categorieName;
		}

		public static ProductOutputDTO FromProduct(Product product,string CategorieName)
		{
			return new ProductOutputDTO(product, CategorieName);
		}
	}
}
