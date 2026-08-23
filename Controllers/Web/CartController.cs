using System.Security.Claims;
using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models.DTO;
using e_commerce_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace e_commerce_system.Controllers.Web
{
	[Route("api/web/[controller]")]
	[ApiController]
	public class CartController:BaseController
	{
		private readonly MainAppDbContet _mainAppcontext;
		private readonly ICartSessionService _cartSessionService;
		private readonly IUserService _userService;
		private readonly ICartService _cartService;

public CartController(MainAppDbContet mainAppcontext, ICartService cartService, IUserService userService, ICartSessionService cartSessionService)
		{
			_mainAppcontext = mainAppcontext;
			_cartService = cartService;
			_userService = userService;
			_cartSessionService = cartSessionService;
		}
//add an item to the cart for the current user or session
		[HttpPost("AddItemToCart")]

		public async Task<IActionResult> AddItemToCart([FromBody]AddItemToCartDTO addItemToCartDTO)

		{


			if (!ModelState.IsValid)
				return CustomBadRequest();

							var product=_mainAppcontext.Products.FirstOrDefault(p => p.ID == addItemToCartDTO.ProductId);

							if(product==null)
								return NotFound(ErrorResponse("Product with ID " + addItemToCartDTO.ProductId + " not found.",StatusCodes.Status404NotFound.ToString()));


				Guid? userId = _userService.GetCurrentUserId();	

				var cartout=await _cartService.AddItemToCart(userId, addItemToCartDTO, product);

			return Ok(SuccessResponse(cartout));;
		} 
			
//get the current cart for the user or session

		[HttpGet]

		public async Task<IActionResult> GetCurrentCart()
		{
			Guid? userId = _userService.GetCurrentUserId();	
			
			var sessionid=userId==null?_cartSessionService.GetOrCreateSessionId():null;

			var cart=await _cartService.GetCartByUserIdOrSessionIdAsync(userId, sessionid);

			if(cart==null)
				return Ok(SuccessResponse("Cart is empty"));

				var cartout=CartOutputDTO.FromCart(cart);


			return Ok(SuccessResponse(cartout));;




		}
		//delete the entire cart for the current user

[HttpDelete("DeleteCart")]

public async Task<IActionResult> DeleteCart()
		{
			var currentUserId = _userService.GetCurrentUserId();
			await _cartService.ClearCartAsync(currentUserId);

			return Ok(SuccessResponse("Cart deleted successfully"));;

	}

	[HttpPatch("DecreaseCartItem/{cartItemId}")]
//it used for decreasing the quantity of a cart item or removing it if the quantity reaches zero
	public async Task<IActionResult> DecreaseCartItem(Guid cartItemId)
		{
			var userid=_userService.GetCurrentUserId();
			
			var cart=await _cartService.GetCurrentCart(userid);
			if(cart==null||!cart.Items.Any())
			{
				return Ok(SuccessResponse("Cart is empty"));
			}
			var cartItem=cart.Items.FirstOrDefault(ci=>ci.ID==cartItemId);	

			if(cartItem==null)
			
				return NotFound(ErrorResponse("Cart item not found",StatusCodes.Status404NotFound.ToString()));

				var updatedCartItem = await _cartService.DeacreaseCartItemQuantityAsync(cartItem);

				if(updatedCartItem==null)
					return Ok(SuccessResponse("Cart item removed from cart"));


			



			return Ok(SuccessResponse(updatedCartItem)	);

		}
//delete a specific cart item from the cart
		[HttpDelete("DeleteCartItem/{cartItemId}")]

		public async Task<IActionResult> DeleteCartItem(Guid cartItemId)
		{
			var userid=_userService.GetCurrentUserId();
			
			var cart=await _cartService.GetCurrentCart(userid);
			if(cart==null||!cart.Items.Any())
			{
				return Ok(SuccessResponse("Cart is empty"));
			}
			var cartItem=cart.Items.FirstOrDefault(ci=>ci.ID==cartItemId);	

			if(cartItem==null)
			
				return NotFound(ErrorResponse("Cart item not found",StatusCodes.Status404NotFound.ToString()));

				_cartService.DeleteCartItem(cartItem);
				await _cartService.SaveChangesAsync();

				return Ok(SuccessResponse("Cart item removed successfully from cart"));
		}
	}


}
