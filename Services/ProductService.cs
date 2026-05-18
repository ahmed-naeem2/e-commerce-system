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
		public async Task<bool> CheckExistProudctAsync(string name)=> await _mainAppDbContet.Products.AnyAsync(p => p.Name.ToLower() == name.ToLower());

		public async Task<Product?> GetProductByIdAsync(Guid Id,CancellationToken token)
		{
		
			return await _mainAppDbContet.Products
				.Include(P=>P.Images)
				.Include(P=>P.Categorie)
				.FirstOrDefaultAsync(P=>P.ID==Id,token);
		}

		public async Task SaveChangeAsync()=> await _mainAppDbContet.SaveChangesAsync();

		

	public async	Task<int> GetProductImageCountAsync(Guid Id)=>await _mainAppDbContet.ProductImages.CountAsync(I=>I.ProductId==Id);

	public	void AddProductImage(ProductImage image)
		{
			_mainAppDbContet.ProductImages.Add(image);

		}


	}
}
