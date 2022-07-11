using System.ComponentModel;
using Newtonsoft.Json;

namespace Core2_Api.Controllers
{
	public class Link
	{
		public static Link To(string routeName, object routeValues = null)
		{
			return new Link
			{
				RouteName = routeName,
				RouteValues = routeValues,
				Method = "GET",
				Relation = null
			};
		}

		[JsonProperty(Order = -6, NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Href { get; set; }

		[JsonProperty(Order = -5, NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		[DefaultValue("GET")]
		public string Method { get; set; }

		[JsonProperty(Order = -4, PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Relation { get; set; }

		[JsonIgnore]
		public string RouteName { get; set; }

		[JsonIgnore]
		public object RouteValues { get; set; }

	}
}
