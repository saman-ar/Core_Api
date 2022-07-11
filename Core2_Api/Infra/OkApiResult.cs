using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core2_Api.Infra
{
	public class OkApiResultTest:OkResult
	{
		public OkApiResultTest() : base()
		{
			Status = 200;
			Message = "همه چی اوکی بود";
		}
		public OkApiResultTest(object Value):base()
		{
			
		}
		public int Status { get; set; }
		public string Message { get; set; }
	}
}
