namespace e_commerce_system.Models
{
	public class Product
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }	

		public Guid CategorieId { get; set; }

		public Categorie? Categorie { get; set; }	

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public double Price { get; set; }

		public  ICollection<ProductImage> Images { get; set; }=new List<ProductImage>();




	}
}
