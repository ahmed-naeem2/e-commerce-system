using e_commerce_system.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace e_commerce_system.Controllers
{
	public class BaseController : ControllerBase
	{


		public ApiResponse<T> SuccessResponse<T>(T Data)
		{

			return new ApiResponse<T>
			{
				IsSuccess = true,
				Result = Data

			};
		}

		public ApiResponse<object> ErrorResponse(string message, string code)
		{

			return new ApiResponse<object>
			{
				IsSuccess = false,
				errors = [  new Error { Message = message, StatusCode = code } ]
			};
		}


		public IActionResult CustomBadRequest()
		{

			var Errors = ModelState.Values
				.SelectMany(x => x.Errors)
				.Select(e => new Error
				{
					Message = e.ErrorMessage,
					StatusCode = StatusCodes.Status400BadRequest.ToString(),

				}).ToList();
			return BadRequest(new ApiResponse<object>
			{
				errors = Errors
			});

		}
	}
}
