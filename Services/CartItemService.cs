
using e_commerce_system.Context;
using e_commerce_system.Models;

namespace e_commerce_system.Services
{
    public class CartItemService:ICartItemService
    {
        private readonly MainAppDbContet _mainAppDbContext;

        public CartItemService(MainAppDbContet mainAppDbContext)
        {
            _mainAppDbContext = mainAppDbContext;
        }

        public void AddCartItem(CartItem cartItem)
        {
            _mainAppDbContext.cartItems.Add(cartItem);
        }

        public async Task SaveChangesAsync()
        {
            await _mainAppDbContext.SaveChangesAsync();
        }

    
        

    }



}