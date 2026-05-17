namespace e_commerce_system.Models.DTO
{
	public class CategorieOutputDTO
	{
		public string Name { get; set; }

		public CategorieOutputDTO() { }

		public CategorieOutputDTO(Categorie categorie) {
			Name = categorie.Name; }

		public static CategorieOutputDTO FromCategorie(Categorie categorie)
		{

			return new CategorieOutputDTO(categorie);
		}
	}
}
