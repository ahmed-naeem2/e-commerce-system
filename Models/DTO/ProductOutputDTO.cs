using System.ComponentModel.DataAnnotations;

namespace e_commerce_system.Models.DTO
{
	public class ProductOutputDTO
	{
		
		public string Name { get; set; }
		
		public string Description { get; set; }

	

		public string CategorieName { get; set; }

		public decimal Price { get; set; }
		
		public List<string>Images { get; set; }

		public ProductOutputDTO() { }

		public ProductOutputDTO(Product product)
		{
			Name=product.Name;
			Description=product.Description;
			Price=product.Price;
			CategorieName=product.Categorie.Name;//error

			Images=product.Images?.Select(i=>i.ImagePath).ToList()??new List<string>();
		}

		public static ProductOutputDTO FromProduct(Product product)
		{
			return new ProductOutputDTO(product);
		}
	}
}
