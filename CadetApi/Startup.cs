using CadetApi.Db;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace CadetApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(CustomExtensionMethods.GetConnectionString(Configuration),
                    name: "CadetApiDB-check",
                    healthQuery: "Select 1;",
                    tags: new string[] { "cadetapidb" });
            services.AddHealthChecksUI();
            services
                .AddCustomDbContext(Configuration)
                .AddControllers();
               

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];
            loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecksUI();//more config needed check link bellow.
                endpoints.MapControllers();
            });

            //https://github.com/xabaril/AspNetCore.Diagnostics.HealthChecks
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                PrepDB.MigrateAndPopulate(app);
            }
        }
    }

    public static class CustomExtensionMethods
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            var server = configuration["DbServer"] ?? "localhost";
            var port = configuration["DbPort"] ?? "1433";
            var user = configuration["DBUser"] ?? "SA";
            var password = configuration["DBPassword"] ?? "D0ntDoThis!";
            var database = configuration["Database"] ?? "ApiDatabase";
            return $"Server={server},{port};Database={database};User Id={user};Password={password}";
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CadetContext>(options =>
            {
                options.UseSqlServer(CustomExtensionMethods.GetConnectionString(configuration),
                       sqlServerOptionsAction: sqlOptions =>
                       {
                           // sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                           //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                           sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                       });
            }
          );

            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // services.Configure<CatalogSettings>(configuration);
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = context =>
            //    {
            //        var problemDetails = new ValidationProblemDetails(context.ModelState)
            //        {
            //            Instance = context.HttpContext.Request.Path,
            //            Status = StatusCodes.Status400BadRequest,
            //            Detail = "Please refer to the errors property for additional details."
            //        };

            //        return new BadRequestObjectResult(problemDetails)
            //        {
            //            ContentTypes = { "application/problem+json", "application/problem+xml" }
            //        };
            //    };
            //});

            return services;
        }
    }
}