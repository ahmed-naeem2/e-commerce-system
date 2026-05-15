using e_commerce_system.Models;

namespace e_commerce_system.IServices
{
	public interface ICategorieService
	{
		 Task <bool> CategoryExistsAsync(string name);
		Task <Categorie?> GetCategorieByNameAsync(string name);
		 void AddCategorie (Categorie categorie);
		Task SaveChangeAsync ();
	}
}
