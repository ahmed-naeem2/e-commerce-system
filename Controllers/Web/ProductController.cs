using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_system.Controllers.Web
{
	[Route("api/web/[controller]/[action]")]
	[ApiController]
	public class ProductController : BaseController
	{


		private readonly IProductService _productService;
		private readonly ICategorieService _categoryService;

		public ProductController(MainAppDbContet mainAppDbContet,IProductService productService,ICategorieService categorieService) { 
		
			_productService= productService;
			_categoryService= categorieService;
		
		}


		[HttpPost]


		public async Task<IActionResult> AddProduct(ProductInputDTO productInput)
		{
			if (!ModelState.IsValid)
			

				return CustomBadRequest();

			var NormailzeCategorieName=productInput.CategorieName.Trim().ToLower();

			var Categorie = await _categoryService.GetCategorieByNameAsync(NormailzeCategorieName);
			if (Categorie is null)
			{
				return BadRequest(ErrorResponse("The Categorie with that name doesn't exist", StatusCodes.Status404NotFound.ToString()));

			}
			Product? NewProduct =Product.FromProductInputDTO(productInput,Categorie.ID);
			

			var IsProductExist =await  _productService.CheckExistProudctAsync(NewProduct.Name);
			

			if (IsProductExist)
			{
				return BadRequest(ErrorResponse("the product with that name already exist ",StatusCodes.Status409Conflict.ToString()));
			}
			

			ProductOutputDTO? ProductOutput=ProductOutputDTO.FromProduct(NewProduct,Categorie.Name);
			_productService.AddProduct(NewProduct);
			await _productService.SaveChangeAsync();



			return Ok(SuccessResponse(ProductOutput));




		}

	}
}
