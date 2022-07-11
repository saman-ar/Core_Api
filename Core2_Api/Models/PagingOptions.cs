using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Models
{
	public class PagingOptions
	{
		[Range(0, 9999, ErrorMessage = "عدد درستی وارد کن")]
		public int? Offset { get; set; } = 0;

		[Range(1, 100, ErrorMessage = "عدد درستی وارد کن")]
		public int? Limit { get; set; } = 10;
	}
}
