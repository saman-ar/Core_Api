using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core2_Api.Dtos;
using Core2_Api.Models;

namespace Core2_Api.Infra
{
	///برای هماهنگی با فیلم ورزن 6.0.2 را نصب کردم
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			///اعضای همنام و هم نوع بصورت اتوماتیک مپ میشوند ولی برای بقیه
			///باید تنظیمات لازم انجام شود
			CreateMap<RoomEntity,Room>();
			//TODO :href prop

			/// برای تمرین
			//CreateMap<RoomEntity, Room>().ForMember("Rate", opt => opt.MapFrom(src => src.Rate / 100));
			CreateMap<RegisterRoom, RoomEntity>();
			CreateMap<UserRegisterDto,AppUser>();

		}
	}
}
