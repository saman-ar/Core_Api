using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Infra
{
	/// بجای این مورد اگر middleware بنویسیم عالی میشه
	/// چون این فیلتر فقط خطاهای داخل متد و کنترلر را دریافت میکند 
	/// و به خطاهایی که در جاهای غیر از اینها اتفاق می افتد کاری ندارد
	public class AppExceptionFilterAttribute : IExceptionFilter //or ExceptionFilterAttribute
	{
		//این متد در هر دو حالت باید توسط ما نوشته شود
		public void OnException(ExceptionContext context)
		{

			context.Result = new ObjectResult(new { statusCode = 500 }) { StatusCode=500 };
		}
	}
}
