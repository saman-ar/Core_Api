using Core2_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core2_Api.Infra
{
	public static class DynamicLinqExpressions
	{
		public static string ToSearchLinqExpression(this IEnumerable<SearchTerm> terms)
		{
			string orderByString = string.Empty;
			string descending = string.Empty;
			foreach (var term in terms)
			{
				//descending = typsof(T).GetTypeInfo().GetProperty("Descending").;
				descending = term.Operator == "Descending" ? "desc" : "";
				orderByString = orderByString + $"{term.Name} {descending} ,";
			}
			orderByString = orderByString.Remove(orderByString.Length - 1);
			return orderByString;
		}

		///نتونستم تکمیل کنم
		//public static string ToSearchLinqExpression(IEnumerable<T> terms)
		//{
		//	string orderByString = string.Empty;
		//	//string descending = string.Empty;
		//	foreach (var term in terms)
		//	{
		//		descending = term.Operator == "Descending" ? "desc" : "";
		//		orderByString = orderByString + $"{term.Name} {descending} ,";
		//	}
		//	orderByString = orderByString.Remove(orderByString.Length - 1);
		//	return orderByString;
		//}
	}
}
