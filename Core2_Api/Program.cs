using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core2_Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core2_Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var webHost = CreateWebHostBuilder(args)
				///برای استفاده از سرور kestrel و کانفیک آن برای 
				///استفاده از http و https
				//.UseKestrel(opts =>
				//{
				//	///for http
				//	opts.Listen(IPAddress.Loopback, 5001);
				//	///for Https and Certificate
				//	opts.Listen(IPAddress.Loopback, 5002, opts => opts.UseHttps("test-certificate.pfx", "test-paassword"));
				//})
				.Build();


			///add some Initialization Config for app
			using (var scope = webHost.Services.CreateScope())
			{
				//var env = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();
				var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

				///for "migrating" before "seeding" database
				///برای مشاهده متد Migrate باید فضای نام زیر اضافه شود
				///using Microsoft.EntityFrameworkCore;
				//context.Database.Migrate();

				if (context != null)
				{
					SeedTestData(context);
				}
			}


			webHost.Run();

			///میتونیم بصورت دستی environmentVaraiable رو اینجا تعیین کنیم 
			///بدون اینکه در prop خود پروژه تنظیم کنیم
			//Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			 WebHost.CreateDefaultBuilder(args)
				  .UseStartup<Startup>();

		private static void SeedTestData(AppDbContext context)
		{

			if (!context.Rooms.Any())
			{
				context.Rooms.AddRange(
					new RoomEntity
					{
						Name = "Hotel-1",
						Rate = 10
					},
					new RoomEntity
					{
						Name = "Hotel-2",
						Rate = 20
					},
					new RoomEntity
					{
						Name = "Hotel-3",
						Rate = 30
					},
					new RoomEntity
					{
						Name = "Hotel-4",
						Rate = 40
					},
					new RoomEntity
					{
						Name = "Hotel-5",
						Rate = 5
					},
					new RoomEntity
					{
						Name = "Hotel-6",
						Rate = 6
					},
					new RoomEntity
					{
						Name = "Hotel-7",
						Rate = 7
					},
					new RoomEntity
					{
						Name = "Hotel-8",
						Rate = 8
					},
					new RoomEntity
					{
						Name = "Hotel-9",
						Rate = 9
					},
					new RoomEntity
					{
						Name = "Hotel-10",
						Rate = 10
					},
					new RoomEntity
					{
						Name = "Hotel-11",
						Rate = 110
					},
					new RoomEntity
					{
						Name = "Hotel-12",
						Rate = 120
					},
					new RoomEntity
					{
						Name = "Hotel-130",
						Rate = 130
					},
					new RoomEntity
					{
						Name = "Hotel-140",
						Rate = 140
					},
					new RoomEntity
					{
						Name = "Hotel-15",
						Rate = 150
					},
					new RoomEntity
					{
						Name = "Hotel-16",
						Rate = 160
					});

				context.SaveChanges();
			}
		}
	}
}
