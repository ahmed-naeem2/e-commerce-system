using e_commerce_system.Context;
using e_commerce_system.IServices;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_system.Controllers.Web
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class OrderController:BaseController
    {
        private readonly MainAppDbContet _mainAppDbContext;
        private readonly IUserService   _userService;
        private readonly IOrderService _orderService;
        

                public OrderController(MainAppDbContet mainAppDbContext, IUserService userService, IOrderService orderService)
        {
            _mainAppDbContext = mainAppDbContext;
            _userService = userService;
            _orderService = orderService;
        }

       [HttpPost("Checkout")] 
       public async Task<IActionResult> Checkout()
        {
            var userId = _userService.GetCurrentUserId();
            if(userId == null)
            
                return Unauthorized(ErrorResponse("You must be logged in to place an order.",StatusCodes.Status401Unauthorized.ToString()));

                var orderoutputDTO = await _orderService.CheckoutAsync(userId.Value);

                if(orderoutputDTO == null)
                
                    return BadRequest(ErrorResponse("Checkout failed. Your cart is empty or an error occurred.",StatusCodes.Status400BadRequest.ToString()));


                






            return Ok(SuccessResponse(orderoutputDTO));

        }
    }
}