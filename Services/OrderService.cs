using e_commerce_system.Context;
using e_commerce_system.IServices;

namespace e_commerce_system.Services
{
    public class OrderService : IOrderService
    {
        private readonly MainAppDbContet _mainAppDbContext;
        private readonly ICartService _cartService; 
        public OrderService(MainAppDbContet mainAppDbContext, ICartService cartService)
        {
            _mainAppDbContext = mainAppDbContext;
            _cartService = cartService;
        {
        }


        public async Task<bool> CheckoutAsync(Guid userId)
        {
            var cart = 
        }
    }
}