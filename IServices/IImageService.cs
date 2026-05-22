using e_commerce_system.Models;

namespace e_commerce_system.IServices
{
	public interface IImageService
	{
		Task<int> GetProductImageCountAsync(Guid Id);
		Task<ProductImage?> GetProductImagebyIdAsync(Guid Id);
		void AddProductImage(ProductImage image);
		Task SaveChangeAsync();
		void DeleteProductImage(ProductImage productImage);
	}
}
