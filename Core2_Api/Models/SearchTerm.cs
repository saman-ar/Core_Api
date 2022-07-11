using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Models
{
	public class SearchTerm
	{
		public string Name { get; set; }
		public string Operator { get; set; }
		public string Value { get; set; }

		public string ToSearchLinqExpression()
		{
			string predicateString = string.Empty;
			switch (Operator.ToLower())
			{
				case "eq":
					predicateString = $"{Name}=={Value}";
					break;
				case "lt":
					predicateString = $"{Name}<{Value}";
					break;
				case "gt":
					predicateString = $"{Name}>{Value}";
					break;
			}

			return predicateString;
		}
	}

}
