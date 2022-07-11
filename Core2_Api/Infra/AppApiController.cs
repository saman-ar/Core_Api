using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core2_Api.Infra;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core2_Api.Infra
{
	/// <summary>
	/// نوشتن یک کنترلر اختصاصی برای api
	/// بطوریکه متدهای okو notfound و badrequest
	/// و غیره خروجی مورد نظر ما رو داشته باشند و قابلیتهای قبلی خود رو هم
	/// داشته باشند
	/// </summary>
	public class AppApiController:ControllerBase
	{
		public OkObjectResult ApiOk()
		{
			var result = new OkApiResult();
			return Ok(result);	
		}

		public OkObjectResult ApiOk(object data)
		{
			var result = new OkApiResult(data);
			return Ok(result);
		}

		public NotFoundObjectResult ApiNotFound()
		{
			var result = new NotFoundApiResult();
			return NotFound(result);
		}

		public BadRequestObjectResult ApiBadRequest()
		{
			var result = new BadRequestApiResult();
			return BadRequest(result);
		}

		public BadRequestObjectResult ApiBadRequest(ModelStateDictionary modelState)
		{
			var result = new BadRequestApiResult(modelState);
			return BadRequest(result);
		}


	}
}
