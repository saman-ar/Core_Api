using Core2_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Dtos
{
	public class Collection<T> : Resource
	{
		public T[] Values;

		public Collection<T> Patterns { get; set; }


	}
}
