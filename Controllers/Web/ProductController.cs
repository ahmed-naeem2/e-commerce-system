using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
using e_commerce_system.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;

namespace e_commerce_system.Controllers.Web
{
	[Route("api/web/[controller]")]
	[ApiController]
	public class ProductController : BaseController
	{


		private readonly IProductService _productService;
		private readonly ICategorieService _categoryService;
		private readonly IFileImageService _fileImageService;
		private readonly MainAppDbContet _mainAppDbContext;
		public ProductController(MainAppDbContet mainAppDbContet,IProductService productService,ICategorieService categorieService,IFileImageService fileImageService,MainAppDbContet mainAppDbContet1) { 
		
			_productService= productService;
			_categoryService= categorieService;
			_fileImageService= fileImageService;
			_mainAppDbContext= mainAppDbContet1;
		
		}


		[HttpPost("Addproduct")]


		public async Task<IActionResult> AddProduct(ProductInputDTO productInput)//add product to without image 
		{
			if (!ModelState.IsValid)
			

				return CustomBadRequest();

			var NormailzeCategorieName=productInput.CategorieName.Trim().ToLower();

			var Categorie = await _categoryService.GetCategorieByNameAsync(NormailzeCategorieName);
			if (Categorie is null)
			{
				return NotFound(ErrorResponse("The Categorie with that name doesn't exist", StatusCodes.Status404NotFound.ToString()));

			}
			Product? NewProduct =Product.FromProductInputDTO(productInput,Categorie.ID);
			

			var IsProductExist =await  _productService.CheckExistProudctAsync(NewProduct.Name);
			

			if (IsProductExist)
			{
				return BadRequest(ErrorResponse("the product with that name already exist ",StatusCodes.Status409Conflict.ToString()));
			}
			

			ProductOutputDTO? ProductOutput=ProductOutputDTO.FromProduct(NewProduct);
			_productService.AddProduct(NewProduct);
			await _productService.SaveChangeAsync();



			return Ok(SuccessResponse(ProductOutput));




		}

		[HttpPost("UploadImageToProduct/{Id}")]

		public async Task<IActionResult>UploadImageToProduct(IFormFile image,Guid Id,CancellationToken token)//add image to specific product 
		{
			if(image==null|| image.Length==0)
				return BadRequest(ErrorResponse("No File Uploaded ",StatusCodes.Status400BadRequest.ToString()));

			var Product=await _productService.GetProductByIdAsync(Id,token);

			var imagecount =await _productService.GetProductImageCountAsync(Id);

			if (imagecount > 3)
			{

				return BadRequest(ErrorResponse("Maximum 3 images allowed for each product.", StatusCodes.Status400BadRequest.ToString()));
			}
			if (Product is null) {

				return BadRequest(ErrorResponse("the product not exist ", StatusCodes.Status404NotFound.ToString()));
			}


			var extion=Path.GetExtension(image.FileName).ToLower();

			if(image.Length> 2 * 1024 * 1024)
			{
				ModelState.AddModelError("", "Max size is 2MB");
				return CustomBadRequest();
			}
			if (!FileExtension.FileExtentions.Contains(extion))
			{
				return BadRequest(ErrorResponse("Invaild image type ", StatusCodes.Status400BadRequest.ToString()));

			}

		var FileImagePath =await 	_fileImageService.SaveImageAsync(image);//upload imag to wwwroot folder 

			var productImage=new ProductImage(Product.ID,FileImagePath);
			_productService.AddProductImage(productImage);

			await _productService.SaveChangeAsync();
			ProductOutputDTO productOutputDTO = ProductOutputDTO.FromProduct(Product);

			return CreatedAtAction(nameof(GetProductById), new {id=Product.ID},productOutputDTO);
			


			
		}


		[HttpGet("Product/{id}")]
		
		//retrive Product Detials By Id 
		public async Task<IActionResult> GetProductById(Guid id,CancellationToken token)
		{
			if (id == Guid.Empty)
			
				return BadRequest(ErrorResponse("Invaild product Id ", StatusCodes.Status400BadRequest.ToString()));
			
			var StoreProduct = await _productService.GetProductByIdAsync(id,token);

			if (StoreProduct is null)

				return NotFound(ErrorResponse($"Product with Id {id} not found .", StatusCodes.Status404NotFound.ToString()));

			var productOutDTO = ProductOutputDTO.FromProduct(StoreProduct);

			return Ok(SuccessResponse(productOutDTO));

		}


		[HttpPut("UpdateProduct/{id}")]

		public async Task< IActionResult>UpdateProduct(Guid id,ProductUpdateDTO productUpdateDTO,CancellationToken token)
		{
			if (id == Guid.Empty)

				return BadRequest(ErrorResponse("the Id can not be Empty ", StatusCodes.Status400BadRequest.ToString()));

			var StoredProduct =await  _productService.GetProductByIdAsync(id,token);

			if(StoredProduct is null)

					return NotFound(ErrorResponse($"Product with this id {id} not found ",StatusCodes.Status404NotFound.ToString()));
			if (!string.IsNullOrEmpty(productUpdateDTO.CategorieName))
			{
				var StoredCategorie = await _categoryService.GetCategorieByNameAsync(productUpdateDTO.CategorieName);

				if (StoredCategorie is null)
					return NotFound(ErrorResponse($"Category '{productUpdateDTO.CategorieName}' does not exist.", StatusCodes.Status404NotFound.ToString()));
				StoredProduct.CategorieId=StoredCategorie.ID;
			}
			StoredProduct.IsActive=productUpdateDTO.IsActive;
		 _productService.CheckUpdateDTO(StoredProduct,productUpdateDTO);

			_productService.UpdateProduct(StoredProduct);
			await _productService.SaveChangeAsync();

			var ProductOutPut = ProductOutputDTO.FromProduct(StoredProduct);

			return Ok(SuccessResponse(ProductOutPut));


		}


	}
}
