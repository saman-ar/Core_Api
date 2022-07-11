using Core2_Api.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core2_Api.Filters
{
	public class ApiResultFilterAttribute : ActionFilterAttribute
	{
		public override void OnResultExecuting(ResultExecutingContext context)
		{
			if (context.Result is StatusCodeResult statusCodeResult)
			{
				var apiResult = new ApiResult2
				{
					IsSuccess = false,
					StatusCode = (ApiResultStatusCode)statusCodeResult.StatusCode,
					Message = "یافت نشد"
				};

				///برای فرستادن درست کد http در قسمت هدر آن باید از کد خط زیر استفاده کنیم
				context.HttpContext.Response.StatusCode = statusCodeResult.StatusCode;

				context.Result = new JsonResult(apiResult);
			}
			else if (context.Result is BadRequestObjectResult badRequestobjectResult)
			{
				var apiResult = new ApiResult2<object>
				{
					IsSuccess = true,
					StatusCode = ApiResultStatusCode.BadRequest,
					Message = "خطا در موارد ارسالی",
					Data = badRequestobjectResult.Value,
				};
				context.Result = new JsonResult(apiResult);

				///برای فرستادن http statuscode درست برای seo
				context.HttpContext.Response.StatusCode = badRequestobjectResult.StatusCode ?? 330;

			}
			else if (context.Result is ObjectResult objectResult)
			{
				var apiResult = new ApiResult2<object>
				{
					IsSuccess = true,
					StatusCode = ApiResultStatusCode.Success,
					Message = ".با موفقیت انجام شد",
					Data = objectResult.Value,
				};
				context.Result = new JsonResult(apiResult);

				///برای فرستادن http statuscode درست برای seo
				context.HttpContext.Response.StatusCode = objectResult.StatusCode ?? 200 ;
			}

			base.OnResultExecuting(context);
		}
	}
}
