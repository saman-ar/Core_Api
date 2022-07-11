using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core2_Api.Models;
using Core2_Api.Dtos;
using Core2_Api.Infra.JwtServices;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Core2_Api.Infra;
using System.Security.Claims;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Core2_Api.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("1")]
	[ApiExplorerSettings(GroupName ="v1")]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext context;
		private readonly IJwtService jwtService;
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;

		public UserController(
					AppDbContext _context,
					IJwtService _jwtService,
					UserManager<AppUser> _userManager,
					SignInManager<AppUser> _signInManager
					)
		{
			context = _context;
			jwtService = _jwtService;
			userManager = _userManager;
			signInManager = _signInManager;
		}

		[HttpGet(Name = nameof(Login))] 
		public async Task<IActionResult> Login(UserLoginDto userDto)
		//public async Task<IActionResult> Login([FromBody]string userName, [FromBody]string password)
		{
			///برای دریافت claim و اعتبار سنجی securityStamp
			//var claimsPricipal =await signInManager.ClaimsFactory.CreateAsync(new AppUser());
			//var user =await signInManager.ValidateSecurityStampAsync(claimsPricipal);

			/// for test
			var user = await userManager.FindByNameAsync(userDto.UserName);
			if (user == null)
				return BadRequest();

			var result = await userManager.CheckPasswordAsync(user, userDto.Password);
			if (!result)
				return BadRequest();

			var jwtToken = await jwtService.GenerateTokenAsync(user);
			if (string.IsNullOrWhiteSpace(jwtToken))
				return StatusCode(500);

			return Content(jwtToken);
		}

        [ValidateAntiForgeryToken]
		[HttpPost(Name = nameof(RegisterUser))]
		//[SwaggerOperation("OpenId", OperationId = "RegisterUser")]
		public async Task<IActionResult> RegisterUser([FromBody]UserRegisterDto userDto)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = Mapper.Map<AppUser>(userDto);

			//await userManager.AddClaimAsync(user, new Claim("Role","Admin"));
			//await userManager.AddClaimAsync(user, new Claim("Role", "Modir"));
			//await userManager.AddClaimAsync(user, new Claim("Section", "Sales"));

			var result = await userManager.CreateAsync(user, userDto.Password);

			if (!result.Succeeded)
				return BadRequest();

			return Ok();
		}
	}
}