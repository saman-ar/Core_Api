using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Core2_Api.Infra;
using System.Net;

namespace Core2_Api.Infra
{
	public class ApiResult
	{
		public ApiResult()
		{}
		public ApiResult(string message, HttpStatusCode status)
		{
			Message = message;
			Status = status;
			IsSucceed = false;
		}
		[JsonProperty(Order =-4)]
		internal bool IsSucceed { get; set; }

		[JsonProperty(Order = -3)]
		internal HttpStatusCode Status { get; set; }

		[JsonProperty(Order = -2)]
		internal string Message { get; set; }

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}

	public class NotFoundApiResult : ApiResult
	{
		public NotFoundApiResult()
		{
			IsSucceed = false;
			Message = "پیدا نشد";
			Status = HttpStatusCode.NotFound;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		public object Data { get; set; }
	}

	public class BadRequestApiResult : ApiResult
	{
		public BadRequestApiResult()
		{
			IsSucceed = false;
			Message = "پارامتر اشتباه ارسال شده است";
			Status = HttpStatusCode.BadRequest;
		}
		public BadRequestApiResult(ModelStateDictionary modelState):this()
		{
			ModelState = modelState;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		public ModelStateDictionary ModelState { get; set; }
	}

	public class OkApiResult : ApiResult
	{
		public OkApiResult()
		{
			IsSucceed = true;
			Message = "با موفقیت انجام شد";
			Status = HttpStatusCode.Ok;
		}
		public OkApiResult(object data) : this()
		{
			Data = data;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		public object Data { get; set; }
	}

	public class InternalServerErrorApiResult : ApiResult
	{
		public InternalServerErrorApiResult()
		{
			IsSucceed = false;
			Message = "خطا در سرور";
			Status = HttpStatusCode.InternalServerError;
		}

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}

	public enum HttpStatusCode
	{
		Ok = 200,
		NoContent=204,
		NotFound = 404,
		BadRequest = 400,
		UnSupportedContent = 415,
		UnAuthorized = 401,
		InternalServerError = 500
	}

	/// برای درک و استفاده از کلاس excepton نوشته شده است 
	/// و در صورت نیاز تکمیل شود
	public class AppException : Exception
	{
		public AppException(string message, int statusCode) : base(message)
		{
			StatusCode = statusCode;
		}
		public int StatusCode { get; set; }
	}
}
