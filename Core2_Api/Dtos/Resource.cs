using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core2_Api.Models
{
	public abstract class Resource
	{
		[JsonProperty(Order =-5)]
		public string href { get; set; }

		[JsonProperty(PropertyName = "rel" , NullValueHandling =NullValueHandling.Ignore , DefaultValueHandling =DefaultValueHandling.Ignore)]
		public string[] Relations { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		[DefaultValue("GET")]
		public string Method { get; set; }

	}
}
