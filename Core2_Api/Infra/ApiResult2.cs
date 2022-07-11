using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core2_Api.Infra
{
	/// <summary>
	/// از فیلم نوشته شده بود و جالبشو نوشتم
	/// در فایل ApiResult
	/// </summary>
	public class ApiResult2
	{
		public bool IsSuccess { get; set; }
		public ApiResultStatusCode StatusCode { get; set; }
		public string Message { get; set; }
		//private static HttpResponse httpResponse;

		//public ApiResult(HttpResponse _httpContext)
		//{
		//	httpResponse = _httpContext;
		//}

		#region Implicite Operator

		public static implicit operator ApiResult2(BadRequestResult value)
		{
			//httpResponse.StatusCode = value.StatusCode;

			return new ApiResult2
			{
				IsSuccess = false,
				StatusCode = ApiResultStatusCode.BadRequest,
				Message = "درخواست جعلی"
			};
		}

		public static implicit operator ApiResult2(NotFoundResult value)
		{
			//httpResponse.StatusCode = value.StatusCode;
			return new ApiResult2
			{
				IsSuccess = false,
				StatusCode = ApiResultStatusCode.NotFound,
				Message = "یافت نشد."
			};
		}

		public static implicit operator ApiResult2(BadRequestObjectResult value)
		{
			//httpResponse.StatusCode = value.StatusCode;
			return new ApiResult2
			{
				IsSuccess = false,
				StatusCode = ApiResultStatusCode.NotFound,
				Message = "یافت نشد."
			};
		}

		#endregion Implicite Operator

	}

	public class ApiResult2<TData> : ApiResult2
	{
		public TData Data { get; set; }

		public static implicit operator ApiResult2<TData>(TData value)
		{
			return new ApiResult2<TData>
			{
				IsSuccess = true,
				StatusCode = ApiResultStatusCode.Success,
				Message = " عملیات با موفقیت انجام شد.",
				Data = value
			};
		}
	}

	public enum ApiResultStatusCode
	{
		Success = 200,
		ServerError = 500,
		BadRequest = 400,
		NotFound = 404,
		ListEmpty = 900
	}
}
