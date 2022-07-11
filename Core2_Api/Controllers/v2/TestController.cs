using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core2_Api.Controllers.v2
{
	 //[ApiExplorerSettings(GroupName ="v2")]
	 [ApiVersion("2")]
    [ApiController]
    public class TestController : Controllers.TestController
    {

		[HttpPost(Name = nameof(TestController.PostTest))]
		//[Obsolete]
		public override ActionResult PostTest(string name)
		{
			var result = name == "saman";
			if (result)
				return BadRequest();

			return NotFound(name);
		}

		[HttpGet("{roomId}",Name = nameof(TestController.GetTest2))]
		public ActionResult GetTest2()
		{
			//var result = name == "saman";
			//if (result)
			//	return BadRequest();

			return NotFound();
		}
	}
}