using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace e_commerce_system.Controllers.Web
{
	[Route("api/web/[controller]/[action]")]
	[ApiController]
	public class CategorieController : BaseController
	{
		
		private readonly ICategorieService _categorieService;

		public CategorieController(MainAppDbContet mainAppDbContet,ICategorieService categorieService)
		{
			
			_categorieService = categorieService;
		}


		[HttpPost]

		public async Task<IActionResult> AddCategorie(CategorieInputDTO categorieInput)
		{
			if(!ModelState.IsValid)

					return CustomBadRequest();

			Categorie?Newcategorie=Categorie.FromCategorieInputDTO(categorieInput);

			var IsCategorieExis = await _categorieService.CategoryExistsAsync(Newcategorie.Name);

			if (!IsCategorieExis)
			{
				

				_categorieService.AddCategorie(Newcategorie);
			await	_categorieService.SaveChangeAsync();

				CategorieOutputDTO? categorieOutput = CategorieOutputDTO.FromCategorie(Newcategorie);

				return Ok(SuccessResponse(categorieOutput));
				
				
			}

			return BadRequest(ErrorResponse("the Categorie Name already Exist ", StatusCodes.Status409Conflict.ToString()));
		}
	}
}
