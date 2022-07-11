using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Core2_Api.Infra;
using Core2_Api.Models;

namespace Core2_Api.Filters
{
	public class JsonExceptionFilter : IExceptionFilter
	{
		private readonly IHostingEnvironment _env;

		public JsonExceptionFilter(IHostingEnvironment env)
		{
			_env = env;
		}
		public void OnException(ExceptionContext context)
		{
			var error = new ApiError();

			if (_env.IsDevelopment())
			{
				error.Message = context.Exception.Message;
				error.StackTrace = context.Exception.StackTrace;
			}
			else
			{
				error.Message = "A server Error Accurred !!";
			}

			context.Result = new ObjectResult(error)
			{  
				StatusCode = 500
			};


			//throw new NotImplementedException();
		}
	}
}
