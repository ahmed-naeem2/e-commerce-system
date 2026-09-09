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
        

                public OrderController(MainAppDbContet mainAppDbContext, IUserService userService)
        {
            _mainAppDbContext = mainAppDbContext;
            _userService = userService;
        }

       [HttpPost("Checkout")] 
       public async Task<IActionResult> Checkout()
        {
            var userId = _userService.GetCurrentUserId();
            if(userId == null)
            
                return Unauthorized(ErrorResponse("You must be logged in to place an order.",StatusCodes.Status401Unauthorized.ToString()));




            return Ok(SuccessResponse("Checkout successful."));

        }
    }
}