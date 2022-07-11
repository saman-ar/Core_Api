using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Core2_Api.Infra
{
	public class AppStatusCodeResult
	{
		public AppStatusCodeResult(HttpStatusCode status, string title)
		{
			Status = status;
			Title = title;
		}

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
		public HttpStatusCode Status { get; set; }

		[JsonProperty]
		public string Title { get; set; }

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
