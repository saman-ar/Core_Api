using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Core2_Api.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("1")]
	[Authorize]
	[ApiExplorerSettings(GroupName = "v1")]
	public class InfoController : ControllerBase
	{
		[ProducesResponseType(typeof(Dtos.UserRegisterDto),StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[SwaggerResponse(200)]
		[Produces("Application/json")]

		[HttpGet(Name =nameof(GetInfo))]
		public IActionResult GetInfo()
		{
			var response = new
			{
				href = Url.Link(nameof(InfoController.GetInfo), null),
				info = new HotelInfo
				{
					//href = Url.Link(nameof(InfoController.GetInfoById), new { Id = 1000 }),
					href = Url.Link(("GetInfoById"), new { Id = 1000 }),
					Title = "sdfsdf",
					Email = "email@example.com"
				}
			};

			return Ok(response);
		}

		[HttpGet("{Id}", Name = nameof(GetInfoById))]
		public IActionResult GetInfoById(int Id)
		{

			return null;
		}


	}
}