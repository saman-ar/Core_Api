using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Infra
{
	/// <summary>
	/// فیلد را برای شرکت در مرتب سازی معرفی می کند
	/// </summary>
	[AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
	public class SortableAttribute : Attribute
	{
		public bool Default { get; set; }
	}
}
