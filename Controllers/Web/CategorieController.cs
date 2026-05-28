using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
using e_commerce_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace e_commerce_system.Controllers.Web
{
	[Route("api/web/[controller]")]
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


		[HttpPut("UpdateCategory/{id}")]

		public async Task<IActionResult> UpdateCategory(Guid id,CategorieInputDTO categorieInputDTO)
		{
			if (id == Guid.Empty)

				return BadRequest(ErrorResponse("the Id can not be Empty ", StatusCodes.Status400BadRequest.ToString()));

			var StoredCategory=await _categorieService.GetCategorieByIdAsync(id);

			if (StoredCategory is null)

				return NotFound(ErrorResponse($"Category wiht This Id {id} not found ", StatusCodes.Status404NotFound.ToString()));

			var NormailzeCategorieName=categorieInputDTO.Name.Trim();

			var IsCategoryNameExist = await _categorieService.CategoryExistsAsync(NormailzeCategorieName);

			if (IsCategoryNameExist)

				return BadRequest(ErrorResponse("Categoriy with This Name already Exist ", StatusCodes.Status409Conflict.ToString()));


			StoredCategory.Name= NormailzeCategorieName;

			_categorieService.UpdateCategory(StoredCategory);
			await _categorieService.SaveChangeAsync();

			var CategoryOutput=CategorieOutputDTO.FromCategorie(StoredCategory);


			return Ok(SuccessResponse(CategoryOutput));


		}
	}
}
