using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2_Api.Models;
using Core2_Api.Dtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;

using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.DynamicLinq;

namespace Core2_Api.Repositories
{
	public interface IRoomRepository
	{
		Task<Room> GetRoomByIdAsync(int roomId);

		Task<IEnumerable<Room>> GetRoomsAsync(
						SortOptions<Room, RoomEntity> sortOptions,
						SearchOptions<Room, RoomEntity> searchOptions);

		RoomEntity Register(RegisterRoom roomDto);
		RoomEntity Delete(int roomId);
	}

	public class RoomRepository : IRoomRepository
	{
		private readonly AppDbContext context;

		public RoomRepository(AppDbContext _context)
		{ context = _context; }

		public async Task<Room> GetRoomByIdAsync(int Id)
		{
			var roomEntity = await context.Rooms.SingleOrDefaultAsync(r => r.Id == Id);
			if (roomEntity == null)
				return null;

			return Mapper.Map<Room>(roomEntity);
		}

		public async Task<IEnumerable<Room>> GetRoomsAsync(
				                        SortOptions<Room, RoomEntity> sortOptions,
				                        SearchOptions<Room, RoomEntity> searchOptions)
		{
			IQueryable<RoomEntity> query = context.Rooms; //get all room entities

			if (!searchOptions.IsNullOrEmpty())
				query = searchOptions.ApplySearching(query);

			if (!sortOptions.IsNullOrEmpty())
				query = sortOptions.ApplyOrdering(query);

			var roomsEntities = query.ProjectTo<Room>();
			if (roomsEntities == null)
				return null;

			return roomsEntities.ToArray();
		}

		public RoomEntity Register(RegisterRoom roomDto)
		{
			var room = new RoomEntity();
			var roomEntity = Mapper.Map<RoomEntity>(roomDto);
			var registeredRoom = context.Rooms.Add(roomEntity);

			var createdRoomCount = context.SaveChanges();
			if (createdRoomCount < 1)
				throw new InvalidOperationException("Can not Create Resource!!");

			return registeredRoom.Entity;
		}

		public RoomEntity Delete(int roomId)
		{
			var room = context.Rooms.SingleOrDefault(r => r.Id == roomId);
			if (room == null) return null;

			context.Rooms.Remove(room);
			var result = context.SaveChanges();

			if (result < 1) return null;

			return room;
		}
	}
}



