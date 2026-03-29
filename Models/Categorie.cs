namespace e_commerce_system.Models
{
	public class Categorie
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public ICollection<Product>Products { get; set; }=new List<Product>();
	}
}
