using DevTest.DbModel;
using DevTest.ScheduleJobs;
using DevTest.Services.UserManagement;
using LTHL.COMMON.Helpers;
using LTHL.VIEW_MODELS.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            #region DbModels Dependency Injection
            services.AddDbContext<DevTestContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BusinessDatabase")));
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevTest", Version = "v1" });
            });

            #region Get MySettings Data
            services.Configure<AppSettings>(Configuration.GetSection("MySettings"));
            #endregion

            #region Validate the JWT Token 
            var appSetting = Configuration.GetSection("MySettings");
            var mySettings = appSetting.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(mySettings.SecretKey);

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidAudience = "Audience",
                       ValidIssuer = "Issuer",
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateIssuerSigningKey = true,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.FromMinutes(0)
                   };

                   //JWT Claims for SignalR Hub
                   options.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context =>
                       {
                           var accessToken = context.Request.Query["access_token"];

                           // If the request is for our hub...
                           var path = context.HttpContext.Request.Path;
                           if (!string.IsNullOrEmpty(accessToken) &&
                         (path.StartsWithSegments("/Hubs/MessageHub")))
                           {
                               // Read the token out of the query string
                               context.Token = accessToken;
                           }
                           return Task.CompletedTask;
                       }
                   };
               });

            #endregion

            #region Services Dependency Injection
            services.AddScoped<IUserManagement, UserManagement>();
            #endregion

            #region ScheduleJob
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                // Create a "key" for the job
                var jobKey = new JobKey("ClaimsJobTrigger");

                // Register the job with the DI container
                q.AddJob<ClaimsJobTrigger>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) // link to the HelloWorldJob
                    .WithIdentity("HelloWorldJob-trigger")
                // give the trigger a unique name
                //.WithDailyTimeIntervalSchedule
                // (s =>
                //    s.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(01, 33))
                //   .WithIntervalInHours(24)
                //   .OnEveryDay()
                //   .InTimeZone(TimeZoneInfo.Utc)
                //  ));
                .WithCronSchedule("0 0/5 * * * ?")); // run every 5 minute 

            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevTest v1"));
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevTest v1"));
            #endregion


            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        var path = context.Request.Path.Value;
                        var controller = path.Split('/')[2];
                        var action = path.Split('/')[3] ?? "";

                        if (string.IsNullOrWhiteSpace(env.WebRootPath))
                        {
                            env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }

                        var userId = context?.GetRouteData()?.Values["userId"]?.ToString();

                        MakeLog Err = new MakeLog();
                        Err.ErrorLog(/*env.WebRootPath*/"C:/inetpub/logs/", " / " + controller + "/" + action + ".txt", "UserId: " + userId + " Error: " + error.Error?.Message ?? "" + "Inner Exception => " + error.Error.InnerException?.Message ?? "");

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<bool>
                        {
                            IsError = true,
                            Message = Error.ServerError,
                            Exception = userId + " Error: " + error.Error?.Message ?? "" + "Inner Exception => " + error.Error.InnerException?.Message ?? "",
                            Data = false
                        }));
                    }
                    //when no error, do next.

                    else await next();
                });
            });

            app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
