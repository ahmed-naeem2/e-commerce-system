using e_commerce_system.Models;

namespace e_commerce_system.IServices
{
	public interface IProductService
	{
		Task<bool> CheckExistProudctAsync(string name);

		void AddProduct(Product product);
		Task SaveChangeAsync();

	}
}
