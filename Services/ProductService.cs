using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
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

		

	

	public	void AddProductImage(ProductImage image)
		{
			_mainAppDbContet.ProductImages.Add(image);

		}

		public void UpdateProduct(Product product)
		{
			_mainAppDbContet.Products.Update(product);
		}
		//this fuction will save and check the updateDTO
		public void CheckUpdateDTO(Product StoredProduct, ProductUpdateDTO productUpdateDTO)
		{
			if (!string.IsNullOrWhiteSpace(productUpdateDTO.Name))
			{
				StoredProduct.Name = productUpdateDTO.Name.Trim();
			}

			if (!string.IsNullOrWhiteSpace(productUpdateDTO.Description))
			{
				StoredProduct.Description = productUpdateDTO.Description.Trim();
			}

			if (productUpdateDTO.Price.HasValue && productUpdateDTO.Price.Value > 0)
			{
				StoredProduct.Price = productUpdateDTO.Price.Value;
			}



		}

		
		
			
		
	}
}
