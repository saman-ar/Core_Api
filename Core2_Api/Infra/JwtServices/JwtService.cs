using Core2_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Core2_Api.Infra.JwtServices
{
	public class JwtService : IJwtService
	{
		private readonly JwtSettings jwtSettings;
		private readonly SignInManager<AppUser> signInManager;
		private readonly UserManager<AppUser> userManager;

		public JwtService(
			IOptionsSnapshot<JwtSettings> _jwtSettings,
			SignInManager<AppUser> _signInManager,
			UserManager<AppUser> _userManager)
		{
			jwtSettings = _jwtSettings.Value;
			signInManager = _signInManager;
			userManager = _userManager;
		}
		public async Task<string> GenerateTokenAsync(AppUser user)
		{
			var descriptor = new SecurityTokenDescriptor
			{
				Issuer = jwtSettings.Issuer, //"saman.com",
				Audience = jwtSettings.Audience, //"saman.com",
				IssuedAt = DateTime.Now,

				//بعد از گذشت این مدت از ایجاد توکن قابل استفاده خواهد بود
				NotBefore = DateTime.Now.AddMinutes(jwtSettings.NoBeforeAsMinute),

				Expires = DateTime.Now.AddMinutes(jwtSettings.ExpiresAsMinute),

				///شامل claim های یوزر میباشد
				Subject =await GetIdentityClaimsAsync(user),

				///بصورت عادی قابل مقدار دهی نیست و باید از کلاس مربوطه اش استفاده کرد
				SigningCredentials = GetSigningCredential(user),

				///تنظیم کدگذاری داده های داخل توکن
				//EncryptingCredentials=GetEncryptionCredential() ,

				TokenType =jwtSettings.TokenType // "JWT",
				
			};

			///کانفیگ jwt
			if (!jwtSettings.MapDotNetClaimTypeToJwt)
			{
				JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
				JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
				JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
			}

			var tokenHandler =new JwtSecurityTokenHandler();
			var securityToken=tokenHandler.CreateToken(descriptor);
			var jwtToken = tokenHandler.WriteToken(securityToken);

			return jwtToken;
		}

		private EncryptingCredentials GetEncryptionCredential()
		{
			var encryptionSecretKey = Encoding.UTF8.GetBytes("SecretKey1234567");///باید فقط 16 کاراکتر باشد

			var encryptionSecurityCredential = new EncryptingCredentials(new SymmetricSecurityKey(encryptionSecretKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

			return encryptionSecurityCredential;
		}

		private async Task<ClaimsIdentity> GetIdentityClaimsAsync(AppUser user)
		{
			var claimsIdentity=await signInManager.ClaimsFactory.CreateAsync(user);
			///برای اینکه امکان اضافه کردن claim جدید را داشته باشیم تبدیل به list میکنیم
			var claims = new List<Claim>(claimsIdentity.Claims);
			//claims.Add(new Claim("", "");

			//var roles = new Role[] { };
			//var list = new List<Claim>()
			//{
			//	new Claim(ClaimTypes.Name , user.UserName),// claim های خود .net
			//	new Claim(ClaimTypes.NameIdentifier,user.Id),// claim های خود .net
			//	new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),// claim های خود .net
			//	new Claim("NickName","samandar"), // یک claim خود تعریف
			//	new Claim(JwtRegisteredClaimNames.Email,"saman@email.com"), //از claim های خودjwt استفاده کرده ایم

			//	new Claim("SecurityStamp" ,user.SecurityStamp),
				
			//};

			/////اضافه کردن Role ها به Claim ها
			//var roles = new Role[]
			//{
			//	new Role { Name="Admin"},
			//	new Role { Name="Sale"},
			//	new Role { Name="Modir"}
			//};

			//foreach (var role in roles)
			//{claims.Add(new Claim(ClaimTypes.Role, role.Name));}

			return new ClaimsIdentity(claims);
		}

		private SigningCredentials GetSigningCredential(AppUser user)
		{
			var secretKey = Encoding.UTF8.GetBytes("SecretKey123456789"); // longer than 16 character
			var signingCredential = new SigningCredentials(new SymmetricSecurityKey(secretKey),SecurityAlgorithms.HmacSha256Signature);

			return signingCredential;
		}
	}
}
