using e_commerce_system.Context;
using e_commerce_system.Enum;
using e_commerce_system.IServices;
using e_commerce_system.Models;
using e_commerce_system.Models.Identity;
using e_commerce_system.Models.Request;
using e_commerce_system.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace e_commerce_system.Controllers.Web
{
	[Route("api/web/[Controller]/[action]")]
	[ApiController]
	public class AccountController : BaseController
	{
		private readonly MainAppDbContet _context;
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly IJwtService _jwtService;
		
		

		public AccountController(MainAppDbContet context,IAuthService authService,IUserService userService,IJwtService jwtService)
		{
			_context = context;
			_authService = authService;

			_userService = userService;
			_jwtService = jwtService;
			
		}


		[HttpPost]

		public async Task <IActionResult> Register(RegistrationReq req)
{

			if (!ModelState.IsValid)

				return CustomBadRequest();

			if(string .IsNullOrEmpty(req.name))
			{
				return BadRequest(ErrorResponse("Name can not be empty ", StatusCodes.Status400BadRequest.ToString()));
			}

			var normailzeName = req.name.ToLower().TrimStart().TrimEnd();//delete all white space from start and end and make the name lower case 

			var normalizeEmail = req.email.ToLower();

			var IsEmailExist = await _authService.CheckUserExistsByEmailAsync(normalizeEmail);
			var IsPhoneNumberExist = await _authService.CheckUserExistsByPhoneNumber(req.PhoneNumber);
			if (IsEmailExist)	
				return BadRequest(ErrorResponse("Email already Used ",  StatusCodes.Status400BadRequest.ToString()));
			

			if (IsPhoneNumberExist)

				return BadRequest(ErrorResponse("Phone Number already Used ", StatusCodes.Status400BadRequest.ToString()));

			var UserName = await _authService.GenerateUserNameAsync(req.name);


			var user = new User(req, UserName.ToString());

			var result =await _authService.CreatUserAsync(user, req.Password);
			if (!result.Succeeded)
			{
				var Errors = result.Errors.Select(e => new Error                //Errors is list Of Class Error now 
				{
					 Message=e.Description,
					StatusCode=StatusCodes.Status400BadRequest.ToString()

				}).ToList();

				return BadRequest(new ApiResponse<object>                 //we return specifi response with errors
				{
					errors = Errors
				});
			}
		await	_authService.AddRoleToUserAsync(user, UserRole.User.ToString());//add role to user 

			return Ok(SuccessResponse<string>("Registration successful"));



		}


		[HttpPost]

		public async Task <IActionResult>Login (LoginRequest req)
		{
			if (!ModelState.IsValid)

				return CustomBadRequest();
			var normalizeIdentifier = req.identifier.TrimEnd().TrimStart();

			var user = await _userService.FindUserAsync(normalizeIdentifier);

			if (user != null)
			{
				if (await _authService.VaildatecredentialsAsync(user, req.Password))
				{
					if (_authService.IsEmail(req.identifier))
					{


					}
					else
					{

					}

				var authenticationResponse =await _authService.LoginResponseAsync(user);

					return Ok(SuccessResponse<AuthenticationResponse>(authenticationResponse));


				}

			}

			return NotFound(ErrorResponse("Invalid credentials", StatusCodes.Status404NotFound.ToString()));
			}

		}
	}

