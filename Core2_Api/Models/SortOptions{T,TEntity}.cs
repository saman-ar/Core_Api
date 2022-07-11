using Core2_Api.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using Core2_Api.Dtos;

namespace Core2_Api.Models
{

	///متد مربوطه یعنی اعمال orderBy در خود Model قرار داده شده است
	public class SortOptions<T, TEntity> : IValidatableObject
	{

		public string[] OrderBy { get; set; }

		/// <summary>
		/// برای انترفیس IValidatableObject استفاده میشود و توسط آن فراخوانی میشود
		/// </summary>
		/// <param name="validationContext"></param>
		/// <returns></returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			ICollection<ValidationResult> validationResults = new List<ValidationResult>();

			if (IsNullOrEmpty())
			{
				validationResults.Add(new ValidationResult("Invalid term."));
				return validationResults;
			}

			var validTermsInProperty = GetSortablePropertyFromModel().Select(p => p.Name);
			var inValidTermsInRequest = GetSortTermsFromRequest().Select(p => p.Name).Except(validTermsInProperty, StringComparer.OrdinalIgnoreCase);

			foreach (var term in inValidTermsInRequest)
			{
				validationResults.Add(new ValidationResult($"{term} is Invalid term.", new[] { nameof(OrderBy) }));
			}

			return validationResults;
		}

		private IEnumerable<SortTerm> GetValidSortTerms()
		{
			var queryTerms = GetSortTermsFromRequest();
			if (queryTerms == null)
				return null;
			//yield break;


			var sortableProperties = GetSortablePropertyFromModel();
			ICollection<SortTerm> checkedTerms = new List<SortTerm>();
			foreach (var term in queryTerms)
			{
				var checkedTerm = sortableProperties.SingleOrDefault(p => p.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));// not casesencetives

				if (checkedTerm == null) continue;

				checkedTerms.Add(new SortTerm
				{
					Name = checkedTerm.Name,
					Descending = term.Descending,
					Default = checkedTerm.Default
				});
				//yield return new SortTerm
				//{
				//	Name = checkedTerm.Name,
				//	Descending = term.Descending,
				//	Default=checkedTerm.Default
				//};
			}

			if (!checkedTerms.Any())
				return null;

			return checkedTerms;
		}

		public IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query)
		{
			var sortTerms = GetValidSortTerms();
			var orderByQueryString = string.Empty;

			if (sortTerms != null)
				orderByQueryString = MakeSearchQueryString(sortTerms);

			query = query.OrderBy(orderByQueryString);
			return query;
		}

		private IEnumerable<SortTerm> GetSortTermsFromRequest()
		{
			if (IsNullOrEmpty())
				return null;

			//if (OrderBy == null)
			//	yield break;

			ICollection<SortTerm> sortTerms = new List<SortTerm>();

			foreach (var term in OrderBy)
			{
				if (string.IsNullOrWhiteSpace(term))
					continue;

				//var sortTerm = new SortTerm();
				var tokens = term.Split(' ');
				if (tokens.Length == 1) //فک کنم باید 1 باشه
				{
					sortTerms.Add(new SortTerm { Name = term });
					continue;
				}
				var descending = tokens.Length > 1 && tokens[1].ToLower().Equals("desc");
				sortTerms.Add(new SortTerm { Name = tokens[0], Descending = descending });
			}

			return sortTerms;
		}

		private IEnumerable<SortTerm> GetSortablePropertyFromModel()
		{
			///با استفاده از رفلکشن بدست میاوریم
			var terms = typeof(T)
				.GetTypeInfo()
				.DeclaredProperties.Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
				.Select(p => new SortTerm
				{
					Name = p.Name,
					Default = p.GetCustomAttribute<SortableAttribute>().Default
				});

			return terms;
		}

		public bool IsNullOrEmpty()
		{
			if (OrderBy == null)
				return true;

			//var isNullOrEmpty = true;
			foreach (var term in OrderBy)
			{
				if (!string.IsNullOrWhiteSpace(term))
					return false;
				//if (string.IsNullOrWhiteSpace(item))
				//	continue;
				//isNullOrEmpty = false;
				//break;
			}
			return true;
		}

		private string MakeSearchQueryString(IEnumerable<SortTerm> sortTerms)
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
