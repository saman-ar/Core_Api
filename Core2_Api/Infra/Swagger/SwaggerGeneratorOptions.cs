﻿using System;
using System.Collections.Generic;
using System.Linq;
using global::Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace Core2_Api.Infra.Swagger
{
	public class SwaggerGeneratorOptions
	{
		public SwaggerGeneratorOptions()
		{
			SwaggerDocs = new Dictionary<string, OpenApiInfo>();
			DocInclusionPredicate = DefaultDocInclusionPredicate;
			OperationIdSelector = DefaultOperationIdSelector;
			TagsSelector = DefaultTagsSelector;
			SortKeySelector = DefaultSortKeySelector;
			Servers = new List<OpenApiServer>();
			SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>();
			SecurityRequirements = new List<OpenApiSecurityRequirement>();
			ParameterFilters = new List<IParameterFilter>();
			RequestBodyFilters = new List<IRequestBodyFilter>();
			OperationFilters = new List<IOperationFilter>();
			DocumentFilters = new List<IDocumentFilter>();
		}

		public IDictionary<string, OpenApiInfo> SwaggerDocs { get; set; }

		public Func<string, ApiDescription, bool> DocInclusionPredicate { get; set; }

		public bool IgnoreObsoleteActions { get; set; }

		public Func<IEnumerable<ApiDescription>, ApiDescription> ConflictingActionsResolver { get; set; }

		public Func<ApiDescription, string> OperationIdSelector { get; set; }

		public Func<ApiDescription, IList<string>> TagsSelector { get; set; }

		public Func<ApiDescription, string> SortKeySelector { get; set; }

		public bool DescribeAllParametersInCamelCase { get; set; }

		public List<OpenApiServer> Servers { get; set; }

		public IDictionary<string, OpenApiSecurityScheme> SecuritySchemes { get; set; }

		public IList<OpenApiSecurityRequirement> SecurityRequirements { get; set; }

		public IList<IParameterFilter> ParameterFilters { get; set; }

		public List<IRequestBodyFilter> RequestBodyFilters { get; set; }

		public List<IOperationFilter> OperationFilters { get; set; }

		public IList<IDocumentFilter> DocumentFilters { get; set; }

		private bool DefaultDocInclusionPredicate(string documentName, ApiDescription apiDescription)
		{
			return apiDescription.GroupName == null || apiDescription.GroupName == documentName;
		}

		private string DefaultOperationIdSelector(ApiDescription apiDescription)
		{
			return apiDescription.ActionDescriptor.AttributeRouteInfo?.Name;
		}

		private IList<string> DefaultTagsSelector(ApiDescription apiDescription)
		{
			return new[] { apiDescription.ActionDescriptor.RouteValues["controller"] };
		}

		private string DefaultSortKeySelector(ApiDescription apiDescription)
		{
			return TagsSelector(apiDescription).First();
		}


		internal Func<IEnumerable<ApiDescription>, string, ApiDescription> ResolveConflict { get; private set; } = ThrowExceptionOnApiDescriptionConflict;

		public void ResolveConflictsBy(Func<IEnumerable<ApiDescription>, string, ApiDescription> resolver) => ResolveConflict = resolver ?? ThrowExceptionOnApiDescriptionConflict;

		private static ApiDescription ThrowExceptionOnApiDescriptionConflict(IEnumerable<ApiDescription> apiDescriptions, string httpMethod)
		{
			throw new NotSupportedException(string.Format(
			  "Multiple operations with path '{0}' and method '{1}'. Are you overloading action methods?",
			  apiDescriptions.First().RelativePathSansQueryString(), httpMethod));
		}



	}
}

