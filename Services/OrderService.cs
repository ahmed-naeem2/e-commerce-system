using e_commerce_system.Context;
using e_commerce_system.Enum;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;

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
        }

        public void AddOrder(Order order)
        {
            _mainAppDbContext.Orders.Add(order);
        }

        public async Task<OrderOutputDTO?> CheckoutAsync(Guid userId)
        {
            var cart = await _cartService.GetCartByUserIdOrSessionIdAsync(userId, null);
            if (cart == null || !cart.Items.Any()) 
            return null;

            var order =Order.FromCart(cart); 
            order.OrderNumber = GenerateOrderNumber("ORD-");

            foreach(var item in cart.Items)
            {
                order.Items.Add(OrderItem.FromCartItem(item,item.Quantity*item.UnitPrice));
        
            }

            AddOrder(order);

            cart.Status = CartStatus.Completed;
            _mainAppDbContext.Carts.Update(cart);
            await SaveChangesAsync();


            return OrderOutputDTO.FromOrder(order);

            


            

          
        }

        public string GenerateOrderNumber(string orderNumber)

        {
            
            var random=new Random();
            string name;

            do
            {
                name=orderNumber+random.Next(1000,9999 );

                

            }while(_mainAppDbContext.Orders.Any(o => o.OrderNumber == name));

            return name;
        }

                    
            


        

        public async Task SaveChangesAsync()
        {
            await _mainAppDbContext.SaveChangesAsync();
        }
    }
}