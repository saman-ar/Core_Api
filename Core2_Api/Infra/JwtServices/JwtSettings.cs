using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2_Api.Infra.JwtServices
{
	public class JwtSettings
	{
		public bool MapDotNetClaimTypeToJwt { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public int NoBeforeAsMinute { get; set; }
		public int ExpiresAsMinute { get; set; }
		public string TokenType { get; set; }
	}
}
