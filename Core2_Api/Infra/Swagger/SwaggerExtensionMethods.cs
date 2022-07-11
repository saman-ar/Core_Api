using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Core2_Api.Infra.Swagger
{
	public static class SwaggerExtensionMethods
	{
		public static void AddMySwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(opts =>
			{
				var xmlPath = Path.Combine(AppContext.BaseDirectory, "Core2_Api.xml");
				opts.IncludeXmlComments(xmlPath, true);

				opts.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "MyApi-V1" });
				opts.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "MyApi-V2" });

				// select actions based on ApiVersion Attribute
				//این روش دردسر دارد و فعلا درست قابل استفاده نیست
				//opts.DocInclusionPredicate((docName, apiDesc) =>
				//{
				//	if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

				//	var versions = methodInfo.DeclaringType
				//		 .GetCustomAttributes(true)
				//		 .OfType<ApiVersionAttribute>()
				//		 .SelectMany(attr => attr.Versions);

				//	return versions.Any(v => $"v{v.ToString()}" == docName);
				//});

				//تعریف خود احراز هویت برای swagger
				//opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				//{
				//	Description = "Bearer , JWT Authentication Example: bearer {token}",
				//	Name = "Authorization",
				//	In = ParameterLocation.Header
				//});

				opts.AddSecurityDefinition("Bearer", new OAuth2Scheme
					{
					  
					});
				opts.OperationFilter<UnauthorizedResponseOperationFilter>(false,"Bearer");

				//opts.AddSecurityRequirement( new Dictionary<string, IEnumerable<string>>
				//	{
				//	  {"Bearer", new string[] {}}
				//	});
				
			});
		}
	}

	internal class OAuth2Scheme : OpenApiSecurityScheme
	{
	}

	public class UnauthorizedResponseOperationFilter : IOperationFilter
	{
		bool IncludeUnauthorizedOperation;
		string SchemeName;
		public UnauthorizedResponseOperationFilter(bool includeUnauthorizedOperation,string schemeName="Bearer")
		{
			IncludeUnauthorizedOperation = includeUnauthorizedOperation;
			SchemeName = schemeName;
		}
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var hasAllowAnonymous = context.ApiDescription.ActionDescriptor.FilterDescriptors.Any(p=>p.Filter is AllowAnonymousAttribute || !(p.Filter is AuthorizeAttribute));

			if (hasAllowAnonymous)
				return;

			//if(IncludeUnauthorizedOperation)



		}
	}


	public class RemoveUnAuthorizedEndpoints : IDocumentFilter
	{
		private HttpRequest httpRequest;
		public RemoveUnAuthorizedEndpoints(HttpRequest _httpRequest)
		{
			httpRequest = _httpRequest;
		}
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			if (httpRequest.HttpContext.User.Identity.IsAuthenticated)
			{ }
			//throw new NotImplementedException();
		}
	}




	//public class MultipleOperationsWithSameVerbFilter : IOperationFilter
	//{
	//	public void Apply(Operation operation, OperationFilterContext context)
	//	{
	//		//if (operation.Parameters != null)
	//		if (operation.Parameters.Any())
	//			{
	//				operation.OperationId += "By";
	//			foreach (var parm in operation.Parameters)
	//			{
	//				operation.OperationId += string.Format("{0}",
	//					 parm.Name);
	//			}
	//		}
	//	}
}
