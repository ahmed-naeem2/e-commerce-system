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

		[HttpPost("AddItemToCart")]

		public async Task<IActionResult> AddItemToCart([FromBody]AddItemToCartDTO addItemToCartDTO)

		{
			if (!ModelState.IsValid)
				return CustomBadRequest();

				Guid? userId = _userService.GetCurrentUserId();	

				var cartout=await _cartService.AddItemToCart(userId, addItemToCartDTO);

			return Ok(SuccessResponse(cartout));;
		} 
			


		[HttpGet]

		public async Task<IActionResult> GetCurrentCart()
		{
			Guid? userId = _userService.GetCurrentUserId();	
			var sessionid=userId==null?_cartSessionService.GetOrCreateSessionId():null;

			var cart=await _cartService.GetCartByUserIdOrSessionIdAsync(userId, sessionid);

			if(cart==null)
				return NotFound(ErrorResponse("no Item Added in cart yet ",StatusCodes.Status404NotFound.ToString()));

				var cartout=CartOutputDTO.FromCart(cart);


			return Ok(SuccessResponse(cartout));;




		}



	}


}
