using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

			var normailzeName =categorieInput.Name.TrimStart().TrimEnd().ToLower();

			var IsCategorieExis = await _categorieService.CategoryExistsAsync(normailzeName);

			if (!IsCategorieExis)
			{
				var Categorie = new Categorie(normailzeName);

				_categorieService.AddCategorie(Categorie);
			await	_categorieService.SaveChangeAsync();

				
				
			}

			return BadRequest(ErrorResponse("the Categorie Name already Exist ", StatusCodes.Status409Conflict.ToString()));
		}
	}
}
