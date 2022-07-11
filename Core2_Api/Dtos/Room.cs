using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2_Api.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Core2_Api.Infra;

namespace Core2_Api.Dtos
{
	public class Room : Resource
	{
		///دراین کلاس id را نیاورده ایم زیرا
		///href  نقش انرا بازی میکند
		///href در کلاس resource قرار دارد

		[JsonIgnore]
		public int Id { get; set; }

		[Sortable]
		[Searchable]
		public string Name { get; set; }


		//[SearchableInt]
		[Sortable(Default = true)]
		[Searchable]
		public int Rate { get; set; }
	}
}
