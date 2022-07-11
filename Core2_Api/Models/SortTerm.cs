using System.Collections.Generic;

namespace Core2_Api.Models
{
	public class SortTerm
	{
		//-------------------
		private string str;//="form private field";
		public int number;
		private bool isValid;

		private void test()
		{ }
		//----------------------

		public string Name { get; set; }
		public bool Descending { get; set; }
		public bool Default { get; set; }

		///قابل استفاده است ولی استفاده نکردیم
		public static string ToSortLinqExpression(IEnumerable<SortTerm> sortTerms)
		{
			string orderByString = string.Empty;
			string descending = string.Empty;
			foreach (var term in sortTerms)
			{
				descending = term.Descending ? "desc" : "";
				orderByString = orderByString + $"{term.Name} {descending} ,";
			}
			orderByString = orderByString.Remove(orderByString.Length - 1);
			return orderByString;
		}
	}
}
