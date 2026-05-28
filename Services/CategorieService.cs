using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Services
{
	public class CategorieService : ICategorieService
	{
		private readonly MainAppDbContet _mainAppDbContet;

		public CategorieService(MainAppDbContet mainAppDbContet)
		{
			_mainAppDbContet = mainAppDbContet;
		}

		public async Task<bool> CategoryExistsAsync(string name)
		{
		return await _mainAppDbContet.Categories
				.AnyAsync(c => c.Name == name);
		}

	public	void AddCategorie(Categorie categorie)
		{
		_mainAppDbContet.Categories.Add(categorie);
		}

		

		public async Task SaveChangeAsync()
		{
			await _mainAppDbContet.SaveChangesAsync();
		}

		public async Task<Categorie?> GetCategorieByNameAsync(string name)
		{
			return await _mainAppDbContet.Categories
				.FirstOrDefaultAsync(C => C.Name == name);
		}

public async 		Task<Categorie?> GetCategorieByIdAsync(Guid id)=> await _mainAppDbContet.Categories.FindAsync(id);

	public	void UpdateCategory(Categorie categorie)=>_mainAppDbContet.Categories.Update(categorie);
		

public 		void DeleteCategorie(Categorie categorie) => _mainAppDbContet.Categories.Remove(categorie);



	}
}
