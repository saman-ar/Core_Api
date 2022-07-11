using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core2_Api.Controllers
{
	[Route("/", Name = "[action]")]
	[ApiVersion("11.0")]
	public class RootController : Controller
	{
		[HttpGet]
		public IActionResult GetRoot()
		{
			var response = new
			{
				href = Url.Link(nameof(RootController.GetRoot), null),
				Rooms = Url.Link(nameof(RoomsController.GetRooms), null),   //new { href = Url.Link(nameof(RoomsController.GetRooms), null) },
				Hotels = Url.Link(nameof(InfoController.GetInfo), null)  //new { href = Url.Link(nameof(InfoController.GetInfo), null) }
			};

			return Ok(response);
		}
	}
}
