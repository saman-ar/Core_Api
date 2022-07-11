using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Dtos
{
	public class UserRegisterDto
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PhoneNumber { get; set; }
	}

	public class UserLoginDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}

