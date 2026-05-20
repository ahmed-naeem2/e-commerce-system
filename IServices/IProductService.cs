using e_commerce_system.Models;
using e_commerce_system.Models.DTO;

namespace e_commerce_system.IServices
{
	public interface IProductService
	{
		Task<bool> CheckExistProudctAsync(string name);

		void AddProduct(Product product);
		Task SaveChangeAsync();
		Task<Product?> GetProductByIdAsync(Guid Id,CancellationToken token);
		Task<int> GetProductImageCountAsync(Guid Id);

		void AddProductImage(ProductImage image);
		void UpdateProduct (Product product);
		void CheckUpdateDTO(Product product, ProductUpdateDTO productUpdateDTO);

	}
}
