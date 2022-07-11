using System.ComponentModel.DataAnnotations;

namespace Core2_Api.Models
{
	public enum GenderType
	{
		[Display(Name ="مرد")]
		Male = -1,

		[Display(Name="زن")]
		Female=10
	}
}
