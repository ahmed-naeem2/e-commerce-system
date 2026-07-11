using e_commerce_system.Models;
using e_commerce_system.Models.DTO;

namespace e_commerce_system.IServices
{
	public interface ICartService
	{
		void AddCart(Cart cart);
		Task SaveChangesAsync();	

		Task<Cart> CreateEmptyCartAsync(Guid? userId, string? sessionId);
		Task<Cart?> GetCartByUserIdOrSessionIdAsync(Guid? userId, string? sessionId);

		Task<CartOutputDTO> AddItemToCart(Guid? userId, AddItemToCartDTO addItemToCartDTO);
		
	}
}
