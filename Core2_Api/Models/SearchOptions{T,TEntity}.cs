using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Core2_Api.Dtos;
using System.Reflection;
using Core2_Api.Infra;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.DynamicLinq;

namespace Core2_Api.Models
{
	public class SearchOptions<T, TEntity> : IValidatableObject
	{
		public string[] Search { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			ICollection<ValidationResult> validationResults = new List<ValidationResult>();
			if (IsNullOrEmpty())
			{
				//validationResults.Add(new ValidationResult("Invalid Search terms."));
				return validationResults;
			}

			var validTermsInProperty = GetSearchablePropertyFromModel().Select(p => p.Name);
			var inValidTermsInRequest = GetSearchTermsFromRequest().Select(p => p.Name).Except(validTermsInProperty, StringComparer.OrdinalIgnoreCase);

			foreach (var term in inValidTermsInRequest)
			{
				validationResults.Add(new ValidationResult($"{term} is Invalid Search terms."));
			}

			return validationResults;
		}

		private IEnumerable<SearchTerm> GetSearchTermsFromRequest()
		{
			if (IsNullOrEmpty())
				return null;

			ICollection<SearchTerm> searchTermsInRequest = new List<SearchTerm>();
			foreach (var term in Search)
			{
				var tokens = term.Split(' ');
				if (tokens.Length == 3)
				{
					searchTermsInRequest.Add(new SearchTerm { Name = tokens[0], Operator = tokens[1], Value = tokens[2] });
				}
			}
			return searchTermsInRequest;
		}

		private SearchTerm GetValidSearchTerms()
		{
			ICollection<SearchTerm> validSearchTerms = new List<SearchTerm>();
			var searchTermsFromRequest = GetSearchTermsFromRequest();
			var SearchablePropertyFromModel = GetSearchablePropertyFromModel();

			if (searchTermsFromRequest == null || SearchablePropertyFromModel == null)
				return null;

			foreach (var term in searchTermsFromRequest)
			{
				var searchTerm = SearchablePropertyFromModel.SingleOrDefault(p => p.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));

				if (searchTerm != null)
					validSearchTerms.Add(new SearchTerm{ Name = term.Name, Operator = term.Operator, Value = term.Value });
			}

			if (validSearchTerms != null)
			{
				var validSearchTerm = validSearchTerms.ToArray();
				return validSearchTerm[0];
			}
			return null;
		}

		public IQueryable<TEntity> ApplySearching(IQueryable<TEntity> query)
		{
			var searchTerm = GetValidSearchTerms();
			if (searchTerm == null)
				return query;

			//var searchQueryString = MakeSearchQueryString();

			query = query.Where(searchTerm.ToSearchLinqExpression());
			return query;
		}

		private IEnumerable<SearchTerm> GetSearchablePropertyFromModel()
		{
			var searchableProperties = typeof(T)
				.GetTypeInfo()
				.DeclaredProperties.Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
				.Select(p => new SearchTerm { Name = p.Name });

			return searchableProperties;

		}

		private string MakeSearchQueryString(SearchTerm searchTerm)
		{
			string predicateString = string.Empty;
			switch (searchTerm.Name.ToLower())
			{
				case "eq":
					predicateString = $"{searchTerm.Name}=={searchTerm.Value}";
					break;
				case "lt":
					predicateString = $"{searchTerm.Name}<={searchTerm.Value}";
					break;
				case "gt":
					predicateString = $"{searchTerm.Name}>={searchTerm.Value}";
					break;
			}
			return predicateString;
		}

		public bool IsNullOrEmpty()
		{
			if (Search == null)
				return true;

			foreach (var term in Search)
			{
				if (!string.IsNullOrWhiteSpace(term))
					return false;
			}
			return true;
		}

	}
}
