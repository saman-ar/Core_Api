using Core2_Api.Filters;
using Core2_Api.Infra;
using Core2_Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Newtonsoft.Json.Serialization;
using Core2_Api.Repositories;
using AutoMapper;
using Core2_Api.Infra.JwtServices;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Core2_Api.Dtos;
using System.Reflection;
using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Core2_Api.Infra.Swagger;

namespace Core2_Api
{
    public class Startup
    {
        private readonly int? httpsPort;
        private readonly IHostingEnvironment env;
        private readonly JwtSettings jwtSettings;
        public Startup(IConfiguration configuration, IHostingEnvironment _env)
        {
            Configuration = configuration;

            ///get ssl port by configuration from launchSetting.json file
            env = _env;
            //if (env.IsDevelopment())
            //{
            //	var launchJsonConfig = new ConfigurationBuilder()
            //								.SetBasePath(env.ContentRootPath)
            //								.AddJsonFile("Properties\\launchSettings.json")
            //								.Build();
            //	httpsPort = launchJsonConfig.GetValue<int>("iisSettings:iisExpress:sslPort");
            //}
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddAutoMapper();

            services.AddIdentity<AppUser, Role>() 
                .AddEntityFrameworkStores<AppDbContext>() 
                .AddDefaultTokenProviders();

            services.AddJwtAuthentication();

            services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));

            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowSomeUrls", options =>
                                    options.WithOrigins("https://www.mysite.com", "http://www.mysite.com")
                                    .AllowAnyMethod()
                                    .AllowCredentials());
            });

            services.AddApiVersioning(opts =>
                        {
                            ///config Versioning for MediaType Versioning
                            //opts.ApiVersionReader = new MediaTypeApiVersionReader("v");
                            //opts.ApiVersionReader = new HeaderApiVersionReader("x-version");
                            //opts.ApiVersionReader = new QueryStringApiVersionReader("version");
                            opts.ApiVersionReader = new UrlSegmentApiVersionReader();

                            /////config Default Version
                            opts.DefaultApiVersion = new ApiVersion(1, 0);
                            opts.AssumeDefaultVersionWhenUnspecified = true;

                            opts.ReportApiVersions = true;

                            //opts.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opts);
                        });


            services.AddMvc(opts =>
                          {
                              ///adding My ExceptionFilter to Global Filters
                              //opts.Filters.Add(typeof(JsonExceptionFilter2));

                              ///setting ssl port that retrieved by configuration file from launchsettings.json file
                              //opts.SslPort = httpsPort;
                              ///adding RequireHttpsFilter as Global Filter
                              //opts.Filters.Add(typeof(RequireHttpsAttribute));

                              ///ابتدا jsonformatter را پیدا کرده و
                              var jsonFormatter = opts.OutputFormatters.OfType<JsonOutputFormatter>().Single();
                              ///سپس حذف میکنیم
                              opts.OutputFormatters.Remove(jsonFormatter);
                              ///بعد از آن بعنوان jsonformatter معرفی میکنیم
                              opts.OutputFormatters.Add(new IonOutputFormatter(jsonFormatter));
                              //opts.RespectBrowserAcceptHeader = true;
                          })
                            /// برای تغییر خروجی از camelcase به pascalcase 
                            /// خط پایین باید  بعد از مت AddMvc بیاید
                            .AddJsonOptions(opts => opts.SerializerSettings.ContractResolver = new DefaultContractResolver())
                            ///adding Xml output formatter
                            //.AddXmlSerializerFormatters()
                            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                            //config ApiController
                            .ConfigureApiBehaviorOptions(options =>
                            {
                                options.SuppressConsumesConstraintForFormFileParameters = true;
                                options.SuppressInferBindingSourcesForParameters = true;
                                options.SuppressModelStateInvalidFilter = true;
                                options.SuppressMapClientErrors = true;
                                options.ClientErrorMapping[404].Link =
                                     "https://httpstatuses.com/404";

                            });

            //services.Configure<MvcJsonOptions>(opts => opts.SerializerSettings.ContractResolver = new DefaultContractResolver());

            ///for Creating/accepting lowercase url
            services.AddRouting(opts =>
            {
                ///برای استفاده در جاوا اسکریپت تنظیم میکنیم
                opts.LowercaseUrls = true;
                opts.LowercaseQueryStrings = true;
            });

            ///adding HSTS configuration
            services.AddHsts(opts =>
                {
                    opts.MaxAge = TimeSpan.FromSeconds(3600);
                    opts.IncludeSubDomains = true;
                    opts.Preload = true;
                    //opts.ExcludedHosts.Add("www.example.com");
                });

            services.AddMySwagger();
            //services.AddSingleton<ISwaggerProvider, MySwaggerGenerator>();

            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IJwtService, JwtService>();

            ///adding Https configuration
            //services.AddHttpsRedirection(opts =>
            //{
            //	opts.RedirectStatusCode = 302;
            //	//opts.HttpsPort = 15000;
            //});

            ///TODO: این مورد کار نمیکنه بررسی بشه
            //services.Configure<Post>(Configuration.GetSection("Post"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseMiddleware<AppExceptionHandlerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseCookiePolicy();

            ///adding CORS middleware before MVC Middleware
            //app.UseCors("AllowSomeUrls");

            //app.UseHsts();
            //app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Doc-v1");
                opts.SwaggerEndpoint("/swagger/v2/swagger.json", "Doc-v2");
                opts.RoutePrefix = string.Empty;
            });

            //app.UseAuthentication();
            app.UseMvc();
        }

        private static void SeedTestData(AppDbContext context)
        {

            context.Rooms.AddRange(
                new RoomEntity()
                {
                    Id = 1,
                    Name = "Hotel-1",
                    Rate = 10
                },
                new RoomEntity()
                {
                    Id = 2,
                    Name = "Hotel-2",
                    Rate = 15
                },
                new RoomEntity()
                {
                    Id = 3,
                    Name = "Hotel-3",
                    Rate = 25
                });

            context.SaveChanges();
        }

        private RequestDelegate SayHelloMiddleware(RequestDelegate arg)
        {
            return async ctx =>
            {
                if (!ctx.Request.IsHttps)
                {
                    //ctx.Response.Redirect(ctx.Request.Path.ToString()+":44358", false);
                    ctx.Response.Redirect("HTTP 302");
                }
                else
                {
                    await arg(ctx);
                    //await ctx.Response.WriteAsync("Hello Saman!! form https request");
                }
            };
        }
    }
}
