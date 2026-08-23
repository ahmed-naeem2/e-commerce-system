using e_commerce_system.Context;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_system.Controllers.Web
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class OrderController:BaseController
    {
        private readonly MainAppDbContet _mainAppDbContext;
        

                public OrderController(MainAppDbContet mainAppDbContext)
        {
            _mainAppDbContext = mainAppDbContext;
        }
    }
}