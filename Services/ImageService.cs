using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Services
{
	public class ImageService : IImageService
	{
		private readonly MainAppDbContet _mainAppDbContet;

		public ImageService(MainAppDbContet mainAppDbContet)
		{
			_mainAppDbContet = mainAppDbContet;
		}

		public async Task<ProductImage?> GetProductImagebyIdAsync(Guid Id) => await _mainAppDbContet.ProductImages.FindAsync(Id);

		public async Task<int> GetProductImageCountAsync(Guid Id) => await _mainAppDbContet.ProductImages.CountAsync(I => I.ProductId == Id);
		public void AddProductImage(ProductImage image)=> _mainAppDbContet.ProductImages.Add(image);

	public async	Task SaveChangeAsync()=>await _mainAppDbContet.SaveChangesAsync();

		public void DeleteProductImage(ProductImage productImage) => _mainAppDbContet.ProductImages.Remove(productImage);
		
	}
}
