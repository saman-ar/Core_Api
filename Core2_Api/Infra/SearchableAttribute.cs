using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Infra
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class SearchableAttribute : Attribute
	{
		public ISearchExpressionProvider ExpressionProvider { get; set; } = new DefaultSearchExpressionProvider();
	}

	[AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
	public class SearchableIntAttribute : SearchableAttribute
	{
		public SearchableIntAttribute()
		{
			ExpressionProvider = new StringToIntSearchExpressionProvider();
		}
	}
}
