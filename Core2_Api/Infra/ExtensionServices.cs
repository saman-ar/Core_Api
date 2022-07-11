using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Core2_Api.Infra.JwtServices;
using Microsoft.AspNetCore.Identity;
using Core2_Api.Models;
using System.Security.Claims;
using System.Reflection;

namespace Core2_Api.Infra
{
	public static class ExtensionServices
	{
		public static void AddJwtAuthentication(this IServiceCollection services)
		{
			services.AddAuthentication(opts =>///تغییر احراز هویت از کوکی به توکن
			{
				opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(opts =>///تنظیمات خود jwt 
			{
				opts.RequireHttpsMetadata = false;
				opts.SaveToken = true;
				opts.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					 {
						 //if (context.Exception != null)
						 // throw context.Exception;

						 return Task.CompletedTask;
					 },
					OnChallenge = context =>
					{
						//var bytesString = Encoding.UTF8.GetBytes("شما توکنی ارائه نداده اید \nشما باید ابتدا لاگین کنید");
						//context.Response.Body.WriteAsync(bytesString);

						return Task.CompletedTask;
					},
					OnMessageReceived = context =>
					{
						//var bytesString = Encoding.UTF8.GetBytes("درخواست شما به دست ما رسید در اسرع وقت رسیدگی میکنیم\n");
						//context.Response.Body.WriteAsync(bytesString);

						return Task.CompletedTask;
					},
					OnTokenValidated = async context =>
					{
						//------------------------
						var signInManger = context.HttpContext.RequestServices.GetRequiredService<SignInManager<AppUser>>();
						if (signInManger == null)
							context.Fail("internal Server Error.");

						var user =await signInManger.ValidateSecurityStampAsync(context.Principal);
						if (user == null)
							context.Fail("UnAuthorized");

						return;
						//------------------------

						///چند خط کد بالا تمامی  کارهای کدهای پایین را انجام میدهد
						/////find user and SecurityStamp claims from context
						//var userName = context.Principal.Identity.Name;
						//var securityStampClaimInContext = context.Principal.Claims.SingleOrDefault(c => c.Type.Equals("AspNet.Identity.SecurityStamp", StringComparison.OrdinalIgnoreCase));
						//if (securityStampClaimInContext == null)
						//{
						//	//context.Response.StatusCode = 401;
						//	//var messageAsBytes = Encoding.UTF8.GetBytes("you have not security stamp");
						//	//await context.Response.Body.WriteAsync(messageAsBytes);

						//	context.Fail("you have not security stamp");
						//	//return;
						//}

						/////find user and SecurityStamp claims from database
						//var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
						//var userInDb = await userManager.FindByNameAsync(userName);
						//if (userInDb == null)
						//	return;
						
						//var securityStampClaimInDb = userInDb.SecurityStamp;// بصورت prop اضافه شده نه بصورت claim 

						/////check SecurityStamp is the same
						//if (securityStampClaimInDb == securityStampClaimInContext.Value)
						//	return;// Task.CompletedTask;

						//context.Response.StatusCode = 401;
						//var stringAsBytes = Encoding.UTF8.GetBytes("SecurityStamp is not same!\nPlease login again");
						//await context.Response.Body.WriteAsync(stringAsBytes);
						//return;
					},
				};
				opts.TokenValidationParameters = GetTokenValidationParameters();
			});
		}

		public static TokenValidationParameters GetTokenValidationParameters()
		{
			var signingSecretKeyAsByte = Encoding.UTF8.GetBytes("SecretKey123456789");// longer than 16 char
			var signingSecurityKey = new SymmetricSecurityKey(signingSecretKeyAsByte);

			var decryptionSecretKeyAsBytes = Encoding.UTF8.GetBytes("SecretKey1234567");//just 16 char
			var decryptionSecurityKey = new SymmetricSecurityKey(decryptionSecretKeyAsBytes);

			var tokenValidationParameters = new TokenValidationParameters ///تمظیمات جهت اعتبار سنجی توکن jwt
			{
				ClockSkew = TimeSpan.Zero,
				RequireSignedTokens = true,

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingSecurityKey,

				/// برای دکریپت کردن قسمت دیتا
				//TokenDecryptionKey = decryptionSecurityKey,

				RequireExpirationTime = true,
				ValidateLifetime = true,

				ValidateAudience = true,
				ValidAudience = "mana.com",

				ValidateIssuer = true,
				ValidIssuer = "mana.com"
			};

			return tokenValidationParameters;
		}
	}
}
