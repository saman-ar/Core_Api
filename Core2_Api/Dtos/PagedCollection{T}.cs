using System;
using System.Collections.Generic;
using Core2_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core2_Api.Dtos
{
	public class PagedCollection<T> : Collection<T>
	{
		private IUrlHelper Url;
		public PagedCollection(IUrlHelper url)
		{
			Url = url;
		}
		
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int? Offset { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int? Limit { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int Size { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string First { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Previous { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Next { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Last { get; set; }

		public PagedCollection<T> CreateNavigationLinks(
				PagingOptions pagingOptions,
				T[] collection,
				int itemsSize,
				string routeName)
		{
			Offset = pagingOptions.Offset + pagingOptions.Limit;
			Limit = pagingOptions.Limit;
			Size = itemsSize;

			GetSelfLink(pagingOptions, itemsSize, routeName);
			GetFirstLink(pagingOptions, itemsSize, routeName);
			GetPreviousLink(pagingOptions, itemsSize, routeName);
			GetNextLink(pagingOptions, itemsSize, routeName);
			GetLastLink(pagingOptions, itemsSize, routeName);
			Values = collection;

			return this;
		}

		private void GetFirstLink(PagingOptions pagingOptions, int itemsSize, string routeName)
		{
			First = pagingOptions.Offset == 0 ? "" : Url.Link(routeName, null);
		}

		private void GetLastLink(PagingOptions pagingOptions, int itemsSize, string routeName)
		{
			Last =pagingOptions.Offset+pagingOptions.Limit < itemsSize ? Url.Link(routeName, new { offset =itemsSize - itemsSize % pagingOptions.Limit , limit = pagingOptions.Limit }) : "";
		}

		private void GetNextLink(PagingOptions pagingOptions, int itemsSize, string routeName)
		{
			Next = pagingOptions.Offset + pagingOptions.Limit > itemsSize ? "" : Url.Link(routeName, new { offset = pagingOptions.Offset + pagingOptions.Limit, limit = pagingOptions.Limit });
		}

		private void GetPreviousLink(PagingOptions pagingOptions, int itemsSize, string routeName)
		{
			Previous = pagingOptions.Offset == 0 ? "" : Url.Link(routeName, new
			{
				offset = pagingOptions.Offset < pagingOptions.Limit ? 0 : pagingOptions.Offset - pagingOptions.Limit,
				limit = pagingOptions.Limit
			});
		}

		private void GetSelfLink(PagingOptions pagingOptions, int itemsSize, string routeName)
		{
			href = Url.Link(routeName, new
			{
				offset = pagingOptions.Offset,
				limit = pagingOptions.Limit
			});
		}
	}
}


