using e_commerce_system.Models.DTO;
using Microsoft.Identity.Client;

namespace e_commerce_system.Models
{
	public class Product
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }	

		public Guid CategorieId { get; set; }

		public Categorie? Categorie { get; set; }	

		public DateTime CreatedAt { get; set; }= DateTime.Now;

		public bool IsActive { get; set; } = true;

		public DateTime UpdatedAt { get; set; }=DateTime.Now;

		public decimal Price { get; set; }

		public  ICollection<ProductImage> Images { get; set; }=new List<ProductImage>();



		public Product() { }

		public Product(ProductInputDTO productInputDTO,Guid categorieId)
		{
			Name= productInputDTO.Name.Trim().ToLower();
			Description= productInputDTO.Description.Trim().ToLower();
			Price= productInputDTO.Price;
			CategorieId=categorieId;

			
		}
		public static Product FromProductInputDTO(ProductInputDTO inputDTO,Guid categorieId)
		{
			return new Product(inputDTO, categorieId);
		}
	}
}
