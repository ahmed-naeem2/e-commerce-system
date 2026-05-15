using e_commerce_system.Context;
using e_commerce_system.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_system.Controllers.Web
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : BaseController
	{


	private readonly MainAppDbContet _mainAppDbContet;

		public ProductController(MainAppDbContet mainAppDbContet) { 
		_mainAppDbContet = mainAppDbContet;

		
		}


		[HttpPost]


		public async Task<IActionResult> AddProduct(ProductInputDTO productInput)
		{
			if(!ModelState.IsValid) {

				return CustomBadRequest();





		}

	}
}
