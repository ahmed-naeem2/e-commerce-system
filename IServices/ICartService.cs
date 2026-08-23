using e_commerce_system.Models;
using e_commerce_system.Models.DTO;

namespace e_commerce_system.IServices
{
	public interface ICartService
	{
		void AddCart(Cart cart);
		void DeleteCart(Cart cart);
		void DeleteCartItem(CartItem cartItem);
		void UpdateCartItem(CartItem cartItem);
		Task SaveChangesAsync();	
		Task <CartItemOutDTO?> DeacreaseCartItemQuantityAsync(CartItem cartItem);
		Task<Cart?> GetCurrentCart(Guid? userId);
		
	Task 	ClearCartAsync(Guid? userId);

		Task<Cart> CreateEmptyCartAsync(Guid? userId, string? sessionId);
		Task<Cart?> GetCartByUserIdOrSessionIdAsync(Guid? userId, string? sessionId);
		

		Task<CartOutputDTO> AddItemToCart(Guid? userId, AddItemToCartDTO addItemToCartDTO,Product product);
		
	}
}
