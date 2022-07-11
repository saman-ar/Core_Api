using Core2_Api.Models;
using System.Threading.Tasks;

namespace Core2_Api.Infra.JwtServices
{
	public interface IJwtService
	{
		Task<string> GenerateTokenAsync(AppUser user);
		//bool ValidateToken();
	}
}