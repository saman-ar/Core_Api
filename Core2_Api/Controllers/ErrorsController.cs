using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace Core2_Api.Controllers
{
	[Route("/[controller]")]
	public class ErrorsController : Controller
	{
		public ActionResult ShowErrorPage()
		{
			return Content("خطای در سرور رخ داده است.");
		}

		[HttpGet("{statusCode}")]
		public ActionResult HandleStatusCode(int statusCode)
		{
			return Content($"Http status Code {statusCode} accured.");

		}


	}
}
