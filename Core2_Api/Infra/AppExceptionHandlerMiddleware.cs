using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Infra
{
	/// <summary>
	/// middleware که برای هندل کردن Exception استفاده میشود
	/// </summary>
	public class AppExceptionHandlerMiddleware
	{
		private readonly RequestDelegate next;
		public AppExceptionHandlerMiddleware(RequestDelegate _next)
		{
			next = _next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);

				#region Handling statusCode

				var statusCode = context.Response.StatusCode;
				string message = string.Empty;

				switch (statusCode)
				{
					case 400:
						message = "پارامتر اشتباه ارسال شده است";
						break;
					//case 401:
					//	message = "امکان دسترسی وجود ندارد";
					//	break;
					//case 404:
					//	message = "پیدا نشد";
					//	break;
					case 415:
						message = "محتوای نامفهوم ارسال شده است";
						break;
					//case 500:
					//	message = "خطا در سرور";
					//	break;
					default:
						message = "خطای ناشناخته";
						break;
				}

				if (!string.IsNullOrWhiteSpace(message))
				{
					var content = new ApiResult(message,(HttpStatusCode)statusCode ).ToJson();
					context.Response.ContentType = "Application/json";

					await context.Response.WriteAsync(content);
				}
				#endregion
			}
			catch (Exception ex)
			{
				var content = new InternalServerErrorApiResult().ToJson();
				context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "Application/json";

				await context.Response.WriteAsync(content);
			}
		}
	}
}
