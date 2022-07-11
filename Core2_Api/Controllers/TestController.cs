using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core2_Api.Controllers
{
	 [Route("api/v{version:apiVersion}/[controller]")]
	 [ApiController]
	 [ApiVersion("1")]
	 //[ApiExplorerSettings(GroupName ="v1")]
	public class TestController : ControllerBase
    {
		//[MapToApiVersion("1")]
		[HttpGet(Name=nameof(TestController.GetTest))]
		public virtual ActionResult GetTest()
		{
			return Ok(new { Name = "Test Version=1" , Link= Url.Link(nameof(TestController.PostTest), null)});
		}

		[HttpPost(Name = nameof(TestController.PostTest))]
		public virtual ActionResult PostTest(string name)
		{
			var result = name == "saman";
			if (result)
				return BadRequest();

			return NotFound(name);
		}
	}

	
}