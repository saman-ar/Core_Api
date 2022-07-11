using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2_Api.Infra
{
	public class IonOutputFormatter : TextOutputFormatter
	{
		private readonly JsonOutputFormatter _jsonOutputFormatter;
		public IonOutputFormatter(JsonOutputFormatter jsonOutputFormatter)
		{ 
			_jsonOutputFormatter = jsonOutputFormatter;
			///prop of base class
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("Application/ion+json"));
			///prop of base class
			SupportedEncodings.Add(Encoding.UTF8);
		}

		public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
		{
			return _jsonOutputFormatter.WriteResponseBodyAsync(context, selectedEncoding);
		}
	}

	public class SamanOutputFormatter : TextOutputFormatter
	{
		private readonly JsonOutputFormatter _jsonOutputFormatter;
		public SamanOutputFormatter(JsonOutputFormatter jsonOutputFormatter)
		{
			_jsonOutputFormatter = jsonOutputFormatter;

			SupportedMediaTypes.Add(new MediaTypeHeaderValue("Application/ion+json"));

			SupportedEncodings.Add(Encoding.UTF8);
		}

		public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
		{
			return _jsonOutputFormatter.WriteResponseBodyAsync(context, selectedEncoding);
			//throw new NotImplementedException();
		}
	}
}
