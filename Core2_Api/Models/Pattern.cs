using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection;

namespace Core2_Api.Models
{
	public class Pattern
	{
		[JsonProperty(Order = -5)]
		public string href { get; set; }

		[JsonProperty(PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string[] Relations { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		[DefaultValue("GET")]
		public string Method { get; set; }

		public FormField[] FormFields { get; set; }

		public void Make<T>(string getRouteName, string postRouteName, string deleteRouteName)
		{

			var type = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttributes<JsonIgnoreAttribute>().Any());




		}
	}

	public class FormField
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsOptional { get; set; }
		public string Description { get; set; }

	}

	#region For Test

	public class Test
	{
		public static int number;
		public Test()
		{

		}
		static Test()
		{
			number = 100;
		}
		public static void Get() { }
	}

	public class Test2
	{
		public Test2(Test test)
		{
			Test.Get();
		}
	}

	#endregion

}
