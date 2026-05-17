using e_commerce_system.Models.DTO;

namespace e_commerce_system.Models
{
	public class Categorie
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public ICollection<Product>Products { get; set; }=new List<Product>();

		public Categorie() { }

		public Categorie(CategorieInputDTO categorieInputDTO)
		{

			Name=categorieInputDTO.Name.Trim().ToLower();
		}

		public static Categorie FromCategorieInputDTO(CategorieInputDTO categorieInputDTO)
		{

			return new Categorie(categorieInputDTO);
		}
	}
}
