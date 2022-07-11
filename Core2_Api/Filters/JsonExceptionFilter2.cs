using Core2_Api.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Core2_Api.Filters
{
	public class JsonExceptionFilter2 : ExceptionFilterAttribute
	{
		private readonly IHostingEnvironment env;

		public JsonExceptionFilter2(IHostingEnvironment _env)
		{
			env = _env;
		}
		public override void OnException(ExceptionContext context)
		{
			ApiError error = new ApiError();
			if (env.IsDevelopment())
			{
				error.Message = context.Exception.Message;
				error.StackTrace = context.Exception.StackTrace;
			}
			else
			{error.Message = "Server Side Error!!";}

			context.Result = new ObjectResult(error)
			{ StatusCode = 500 };

			base.OnException(context);
		}
	}
}
