using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2_Api.Filters;
using Core2_Api.Infra;
using Core2_Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace Core2_Api.Controllers
{
	/// برای اینکه نامگذاری route ها بصورت یکجا تعریف شود 
	/// میتوان انرا روی controller بصورت زیر تعریف کرد
	//[Route("/[Controller]", Name ="[Action]")]

	[Route("/[Controller]")]
	[ApiController]
	//[ApiVersion("1.0")]
	//[RequireHttps] /// forcing this controller to use Https
	public class RoomController : AppApiController
	{
		private readonly AppDbContext context;

		public RoomController(AppDbContext _context)
		{
			context = _context;
		}

		//[AutoValidateAntiforgeryToken]
		//[IgnoreAntiforgeryToken]
		[HttpGet(Name = nameof(GetRooms))]
		public IActionResult GetRooms()
		{

			///testing Bcrypt Password Hasher
			//var hashPassword=BCrypt.Net.BCrypt.HashPassword("");
			//bool isTrue=BCrypt.Net.BCrypt.Verify("", "");
			//BCrypt.Net.BCrypt.GenerateSalt(13);



			var rooms = context.Rooms.ToList();

			if (rooms.Count == 0)
				///اگر هیچ چیزی در پارامتر NotFound فرستاده نشود خروجی از نوع کد
				///406 خواهد بود
				///واگر رشته فرستاده شود خروجی بصورت plain text خواهد بود
				///اگر شی فرستاده شود خروجی بصورت json خواهد بود
				return NotFound(new AppStatusCodeResult(AppStatusCode.NotFound, "متاسفانه پیدا نشد."));
			//return BadRequest("bad request");

			return Ok(rooms);



			//return NotFound();
			//return BadRequest(ModelState);

			//return new ApiResult<Post>
			//{
			//	IsSuccess = true,
			//	StatusCode = ApiResultStatusCode.Success,
			//	Message = "Message from Api Result",
			//	Data = new Post
			//	{
			//		Id = 1,
			//		Title = "My title",
			//		Description = "My description"
			//	}
			//};
		}

		//[HttpGet("{Id}")]
		[HttpPost]
		//public ApiResult<Post> GetRoomById(int Id)
		//[ApiResultFilter]
		//public ActionResult<List<Post>> GetRoomById(int Id)
		//[Produces("Application/xml")] ///باعث میشود همیشه خروجی xml داشته باشیم
		//public ActionResult<List<Post>> GetRoomById(Model model)
		public IActionResult GetRoomById(Modell model)

		{
			//if (true) ;
			//return BadRequest(string.Empty);
			//return StatusCode(201,new OkApiResult());
			//return Ok(new Post {  Id=2, Title="sdfd", Description="ddfgdfg"});

			//return BadRequest(new AppStatusCodeResult(AppStatusCode.BadRequest, "پارامتر اشتباه"));
			//ModelState.AddModelError("","Error-1");
			//return BadRequest(new BadRequestApiResult(ModelState));
			//return NotFound(new NotFoundApiResult());

			//throw new Exception();

			List<Post> posts = new List<Post>
			{
				new Post
				{
					Id =model.Id,
					Title = "My Post",
					Description = "My Post description"
				},
				new Post
				{
					Id =model.Id,
					Title = "My Post2",
					Description = "My Post description2"
				}
			};

			return ApiBadRequest();
			//return ApiNotFound();
			//return StatusCode(202,new OkApiResult(posts));
		}
	}
	public class Modell
	{
		public int Id { get; set; }
	}
}
