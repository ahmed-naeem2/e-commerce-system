using System.Security.Claims;
using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace e_commerce_system.Controllers.Web
{
	[Route("api/web/[controller]")]
	[ApiController]
	public class CartController:BaseController
	{
		private readonly MainAppDbContet _mainAppcontext;
		private readonly ICartService _cartService;

public CartController(MainAppDbContet mainAppcontext, ICartService cartService)
		{
			_mainAppcontext = mainAppcontext;
			_cartService = cartService;
		}
		[HttpPost("AddItemToCart")]

		public async Task<IActionResult> AddItemToCart([FromBody]AddItemToCartDTO addItemToCartDTO)
		{
			if (!ModelState.IsValid)
				return CustomBadRequest();

Guid? userId = null;

			if (User.Identity?.IsAuthenticated == true)
			{
				var CureentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
							if (Guid.TryParse(CureentUserId, out Guid parsedUserId))
				{
					
					userId = parsedUserId;
				}
							
							}

							var cartout=await _cartService.AddItemToCart(userId, addItemToCartDTO);

							

			return Ok(SuccessResponse(cartout));;
		} 
			


		

	}


}
