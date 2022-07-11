using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2_Api.Dtos;
using Core2_Api.Filters;
using Core2_Api.Infra;
using Core2_Api.Models;
using Core2_Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.Extensions.Options;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace Core2_Api.Controllers
{
	/// برای اینکه نامگذاری route ها بصورت یکجا تعریف شود 
	/// میتوان انرا روی controller بصورت زیر تعریف کرد
	//[Route("/[Controller]", Name ="[Action]")]
	//[Produces(MediaTypeNames.Application.Json)]
	[ApiController]
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[Controller]")]
	[ApiExplorerSettings(GroupName ="v1")]
	public class RoomsController : ControllerBase
	{
		private readonly AppDbContext context;
		private readonly IRoomRepository repository;

		public RoomsController(AppDbContext _context, IRoomRepository _repository)
		{
			context = _context;
			repository = _repository;
		}

		[HttpGet(Name = nameof(GetRooms))]
		[MapToApiVersion("2")]
		//[MapToApiVersion("2.1")]
		public virtual async Task<IActionResult> GetRooms(
					[FromQuery]PagingOptions pagingOptions,
					[FromQuery]SortOptions<Room, RoomEntity> sortOptions,
					[FromQuery]SearchOptions<Room, RoomEntity> searchOptions)
		{
            if (!ModelState.IsValid)
                return BadRequest(new BadRequestApiResult());//StatusCode(400);//

            var orderedRooms = await repository.GetRoomsAsync(sortOptions, searchOptions);
			if (orderedRooms.Count() == 0)
				return NotFound(new AppStatusCodeResult(HttpStatusCode.NotFound, "اتاقی ثبت نشده است."));

			var pagedOrderedRooms = orderedRooms
											.Skip(pagingOptions.Offset.Value)
											.Take(pagingOptions.Limit.Value);

			pagedOrderedRooms = WriteAllHref(nameof(GetRoomById), pagedOrderedRooms);

			var routeName = nameof(GetRooms);
			var pagedCollection = new PagedCollection<Room>(Url)
				.CreateNavigationLinks(pagingOptions, pagedOrderedRooms.ToArray(), orderedRooms.Count(), routeName);

			return Ok(pagedCollection);

			#region For Test
			///testing Bcrypt Password Hasher
			//var hashPassword=BCrypt.Net.BCrypt.HashPassword("");
			//bool isTrue=BCrypt.Net.BCrypt.Verify("", "");
			//BCrypt.Net.BCrypt.GenerateSalt(13);
			#endregion
		}

		// GET /Rooms/{roomId}
		[HttpGet("{roomId}", Name = nameof(GetRoomById))]
		public virtual async Task<IActionResult> GetRoomById(int roomId)
		{
			if (!ModelState.IsValid)
				return BadRequest(new BadRequestApiResult(ModelState));

            #region For test

            //context.Entry(room).Collection("").Query().Count();
            //context.Entry(room).Reference("").Load();
            //if (room.Any())
            //{
            //	//context.Entry(room).Collection();
            //}
            #endregion

            var roomDto = await repository.GetRoomByIdAsync(roomId);
			if (roomDto == null)
				return NotFound(new NotFoundApiResult());

			roomDto = WriteHref(nameof(GetRoomById), roomDto);

			var response = new Collection<Room>
			{
				href = Url.Link(nameof(GetRoomById), null),
				Values = new Room[] { roomDto }
			};

			return Ok(roomDto);
		}

		[HttpPost("Register", Name = nameof(Register))]
		[Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
		[Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
		public virtual IActionResult Register([FromBody]RegisterRoom registerRoom)
		{
			if (!ModelState.IsValid) return BadRequest();

			//var room = repository.GetRoomByIdAsync(Id);
			//if (room == null) return NotFound();

			var registeredRoom = repository.Register(registerRoom);
			return Created(Url.Link(nameof(GetRoomById), new { roomId = registeredRoom.Id }), new OkApiResult(registeredRoom));
		}

		[HttpDelete("delete/{roomId}", Name = nameof(Delete))]
		public virtual IActionResult Delete(int roomId)
		{
			var result = repository.Delete(roomId);

			if (result == null)
				return BadRequest(new InternalServerErrorApiResult());

			return Ok(new OkApiResult(result));
			//return NoContent();
		}

		private Room WriteHref(string routeName, Room roomDto)
		{
			roomDto.href = Url.Link(routeName, roomDto.Id);
			return roomDto;
		}

		private IEnumerable<Room> WriteAllHref(string routeName, IEnumerable<Room> rooms)
		{
            rooms.ToList().ForEach(r => r.href = Url.Link(routeName, new { roomId = r.Id }));

            return rooms;
		}
	}
}
