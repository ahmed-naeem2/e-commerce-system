using e_commerce_system.Context;
using e_commerce_system.Enum;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Services
{
	public class CartService:ICartService
	{
		private readonly MainAppDbContet _mainAppDbContext;
		private readonly ICartSessionService _cartSessionService;

		public CartService(MainAppDbContet mainAppDbContext, ICartSessionService cartSessionService)
		{
			_mainAppDbContext = mainAppDbContext;
			_cartSessionService = cartSessionService;
		}

		public void AddCart(Cart cart)
		{
			_mainAppDbContext.Carts.Add(cart);
		}

        public async Task<CartOutputDTO> AddItemToCart(Guid? userId, AddItemToCartDTO addItemToCartDTO)
		{
			var porductId=_mainAppDbContext.Products.FirstOrDefault(p => p.ID == addItemToCartDTO.ProductId);
			if (porductId == null)
			
				throw new Exception("Product with ID " + addItemToCartDTO.ProductId + " not found.");

				var Sessionid= userId==null?_cartSessionService.GetOrCreateSessionId():null;

				var cart= await GetCartByUserIdOrSessionIdAsync(userId,Sessionid);

				if(cart==null)
				
					cart =await CreateEmptyCartAsync(userId, Sessionid);
					
					var existingCartItem=cart.Items.FirstOrDefault(ci=>ci.ProductId==addItemToCartDTO.ProductId);

					if(existingCartItem!=null)
					{
						existingCartItem.Quantity+=addItemToCartDTO.Quantity;
						existingCartItem.UpdatedAt=DateTime.UtcNow;
					}else{

					cart.Items.Add(new CartItem
					{
						ProductId=addItemToCartDTO.ProductId,
						Quantity=addItemToCartDTO.Quantity,
						CreatedAt=DateTime.UtcNow,
						UpdatedAt=DateTime.UtcNow,

                        UnitPrice = porductId.Price
						});
					

					}
					await SaveChangesAsync();

					var Carout= CartOutputDTO.FromCart(cart);

					return Carout;


					
					}
				


			
		}

        public async Task<Cart> CreateEmptyCartAsync(Guid? userId, string? sessionId)
        {
			var newCart = new Cart
			{
				UserId = userId,
				SessionId = sessionId,
				Status = CartStatus.Active,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
			AddCart(newCart);
			await SaveChangesAsync(); // Save the new cart to the database


			return newCart;
		}

        public Task<Cart?> GetCartByUserIdOrSessionIdAsync(Guid? userId, string? sessionId)=>_mainAppDbContext.Carts
		.Include(c => c.Items).
		ThenInclude(ci => ci.product)
		.ThenInclude(p=>p.Images).
				FirstOrDefaultAsync(c=>c.Status==CartStatus.Active &&
				userId !=null?c.UserId==userId:
				c.SessionId==sessionId);
        
    

        public async Task SaveChangesAsync()
		{
			await _mainAppDbContext.SaveChangesAsync();
		}
	
	}
}
