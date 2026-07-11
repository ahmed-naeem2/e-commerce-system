using e_commerce_system.Models;

namespace e_commerce_system.Services
{
    public interface ICartItemService
    {
        void AddCartItem(CartItem cartItem);
        Task SaveChangesAsync();
    }


}