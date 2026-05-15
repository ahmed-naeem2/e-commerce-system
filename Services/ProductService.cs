using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Services
{
	public class ProductService : IProductService
	{
		private readonly MainAppDbContet _mainAppDbContet;

		public ProductService(MainAppDbContet mainAppDbContet)
		{
			_mainAppDbContet = mainAppDbContet;
		}

		public void AddProduct(Product product) => _mainAppDbContet.Products.Add(product);
		public async Task<bool> CheckExistProudctAsync(string name)=> await _mainAppDbContet.Products.AnyAsync(p => p.Name == name);
		public async Task SaveChangeAsync()=> await _mainAppDbContet.SaveChangesAsync();



	}
}
