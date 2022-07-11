using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Diagnostics;

namespace Core2_Api.Infra
{
	public class DisableStatusCodePageMiddlewareAttribute:ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var feature = context.HttpContext.Features.Get<IStatusCodePagesFeature>();
			if (feature!=null)
			{
				feature.Enabled = false;
			}
			base.OnActionExecuting(context);
		}
	}
}
