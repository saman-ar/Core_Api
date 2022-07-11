using Core2_Api.Models;

namespace Core2_Api.Controllers
{
	internal class RootResponse : Resource
	{
		public Link Rooms { get; set; }
		public Link Hotels { get; set; }
	}
}
