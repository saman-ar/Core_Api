using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Dtos
{
	public class RegisterRoom
	{
		//public int Id { get; set; }
		[Required]
		[Display(Name = "RoomName", Description = "Name of room")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "RoomRate", Description = "Rate of Room")]
		public int Rate { get; set; }
	}
}
