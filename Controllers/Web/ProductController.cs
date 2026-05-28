using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
using e_commerce_system.QueryFilter;
using e_commerce_system.Services;
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
		private readonly IImageService _imageService;
		private readonly PaginationService _paginationService;
		public ProductController(MainAppDbContet mainAppDbContet,IProductService productService,ICategorieService categorieService,IFileImageService fileImageService,MainAppDbContet mainAppDbContet1,IImageService imageService,PaginationService paginationService) { 
		
			_productService= productService;
			_categoryService= categorieService;
			_fileImageService= fileImageService;
			_mainAppDbContext= mainAppDbContet1;
			_imageService= imageService;
			_paginationService= paginationService;
		
		}


		

		[HttpGet("Products")]

		public async Task<IActionResult> GetProductsPage([FromQuery]ProductQueryFilter productQueryFilter,CancellationToken cancellationToken)
		{
			var product=await _paginationService.GetAllProduct(productQueryFilter,cancellationToken);

			return Ok(SuccessResponse(product));


		}
		[HttpPost("Addproduct")]
		public async Task<IActionResult> AddProduct([FromBody]ProductInputDTO productInput)//add product to without image 
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

			var imagecount =await _imageService.GetProductImageCountAsync(Id);

			if (imagecount >= 3)
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
			_imageService.AddProductImage(productImage);

			await _imageService.SaveChangeAsync();
			ProductOutputDTO productOutputDTO = ProductOutputDTO.FromProduct(Product);

			return Ok(SuccessResponse(productOutputDTO));
			


			
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

			var StoredProduct =await  _productService.GetProductByIdAsync(id,token);//this fucntion return object of product with Categorie and Images 

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
		[HttpDelete("Product/{id}")]

		//public async Task<IActionResult> DeletProduct(Guid id, CancellationToken token)
		//{
		//	if (id == Guid.Empty)
		//		return BadRequest(ErrorResponse("Id can't be empty ", StatusCodes.Status400BadRequest.ToString()));

		//	var StoredProduct = await _productService.GetProductByIdAsync(id, token);
		//	if (StoredProduct is null)

		//		return NotFound(ErrorResponse($"Product with this Id {id} Not Found", StatusCodes.Status404NotFound.ToString()));

		//	foreach (var image in StoredProduct.Images)
		//	{

		//		FIleServiceImage.DeleteImagePath(image.ImagePath);

		//	}


		//	_productService.RemoveProduct(StoredProduct);
		//	await _productService.SaveChangeAsync();

		//	return Ok(SuccessResponse("the Product Deleted successfully "));

		//}

		[HttpDelete("DeletImageProduct/{id}")]

		public async Task <IActionResult>DeleteImageProduct(Guid id)
		{
			if (id == Guid.Empty)
				return BadRequest(ErrorResponse("the Id can't be empty", StatusCodes.Status404NotFound.ToString()));

			var StoredProductImage=await _imageService.GetProductImagebyIdAsync(id);

			if(StoredProductImage is null)

					return NotFound(ErrorResponse($"the with this Id {id} not found ",StatusCodes.Status404NotFound.ToString()));

			FIleServiceImage.DeleteImagePath(StoredProductImage.ImagePath);//here delete the image from wwwroot folder 

			_imageService.DeleteProductImage(StoredProductImage);

			await _imageService.SaveChangeAsync();

			return Ok(SuccessResponse("Image Deleted Successfully"));


		}

	}
}
